using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UM.Core.Domain.Models;

namespace UM.Presentation.WebApi.Extensions.Services;

public static class JwtAuthenticationExtensions
{
    /// <summary>
    /// ავთენთიფიკაციის პარამეტრების დამატება
    /// </summary>
    public static void AddJwtAuthenticationConfigs(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Token:Issuer"],
                    ValidAudience = Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ClockSkew = TimeSpan.Zero // ანულებს ტოკენის სიცოცხლის ხანგრძლივობას. დეფოლტად არის 5 წუთი
                };
            });
    }

    /// <summary>
    /// ავტორიზაციის პარამეტრების დამატება
    /// </summary>
    public static void AddJwtAuthorizationConfigs(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            // მისი გამოყენება მოხდება [Authorize(Policy = "AdminUserPolicy")] ატრიბუტით
            options.AddPolicy("AdminUserPolicy",
                policy =>
                {
                    policy.RequireAssertion(con =>
                        con.User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "1"));
                });
        });
    }

    /// <summary>
    /// ტოკენის გენერირება
    /// </summary>
    public static string GenerateJwtToken(
        IConfiguration configuration,
        string personId,
        string userName,
        string email,
        string firstName,
        string lastName,
        string privateNumber,
        int roleId
    )
    {
        List<Claim> claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, personId),
            new("UserName", userName),
            new("email", email),
            new("FirstName", firstName),
            new("LastName", lastName),
            new("PrivateNumber", privateNumber),
            new(ClaimTypes.Role, roleId.ToString())
        };
        


        // ქმნის JWT ხელმოწერას
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
        var signinCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken
        (
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            issuer: configuration["Token:Issuer"],
            audience: configuration["Token:Audience"],
            signingCredentials: signinCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
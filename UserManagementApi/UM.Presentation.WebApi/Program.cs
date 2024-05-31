using Microsoft.OpenApi.Models;
using UM.Core.Application;
using UM.Core.Application.Validators;
using UM.Infrastructure.Persistence;
using UM.Presentation.WebApi.Extensions.Middlewares;
using UM.Presentation.WebApi.Extensions.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "UM API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myPolicy", builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .WithExposedHeaders("Authorization", "AccessToken", "PageIndex", "PageSize", "TotalPages", "TotalCount", "HasPreviousPage", "HasNextPage"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddServiceLayer(builder.Configuration);
builder.Services.AddPersistanceLayer(builder.Configuration);
builder.Services.AddScoped<UserRequestValidator>();
builder.Services.AddJwtAuthenticationConfigs(builder.Configuration);
builder.Services.AddJwtAuthorizationConfigs();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("_myPolicy");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

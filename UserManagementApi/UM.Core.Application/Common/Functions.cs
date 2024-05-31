using System.Security.Cryptography;

namespace UM.Core.Application.Common;

public static class Functions
{
    private const int SaltSize = 16;
    private const int KeySize = 64;
    private const int Iterations = 310000; 

    public static string GetPasswordHash(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(
                   password,
                   SaltSize,
                   Iterations,
                   HashAlgorithmName.SHA512))
        {
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);
            return $"{Iterations}.{salt}.{key}";
        }
    }
}
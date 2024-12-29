using System.Security.Cryptography;
using System.Text;

namespace HumanResourcesWebApi.Common.AuthenticationService
{
    public static class AuthService
    {
        public static string HashPassword(string password, string salt)
        {
            // Convert salt to bytes
            var saltBytes = Convert.FromBase64String(salt);

            // Generate hashed password using PBKDF2
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100000, HashAlgorithmName.SHA256))
            {
                var hashedPasswordBytes = pbkdf2.GetBytes(32); // 256-bit hash
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        public static string GenerateSalt()
        {
            var saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
    }
}

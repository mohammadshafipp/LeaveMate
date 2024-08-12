using System.Security.Cryptography;
using System.IO;

namespace LeaveMate.Others
{
    public class PublicFunctions
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            string HashedPassword = "";
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("Password cannot be null or empty", nameof(password));
                }

                using (var rng = new RNGCryptoServiceProvider())
                {
                    var salt = new byte[SaltSize];
                    rng.GetBytes(salt);

                    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                    var hash = pbkdf2.GetBytes(HashSize);

                    var hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                    HashedPassword = Convert.ToBase64String(hashBytes);
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return HashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                var hashBytes = Convert.FromBase64String(hashedPassword);

                var salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                var storedHash = new byte[HashSize];
                Array.Copy(hashBytes, SaltSize, storedHash, 0, HashSize);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                var hash = pbkdf2.GetBytes(HashSize);

                for (int i = 0; i < HashSize; i++)
                {
                    if (hash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }
            catch(Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
                return false;
            }

            return true;
        }
    }
}
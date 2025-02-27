using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace EShop.Infrastructure.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 128 / 8; // 16 bytes
        private const int Iterations = 100_000;
        private const int KeySize = 256 / 8; // 32 bytes
        private const string Pepper = "@A#1&B@xz";


        /// <summary>
        /// هش کردن رمز عبور 
        /// </summary>
        /// <param name="password">رمز عبور ورودی</param>
        /// <returns>رشته هش ‌شده رمز عبور</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("رمز عبور نمی‌تواند خالی باشد", nameof(password));

            // تولید Salt تصادفی
            byte[] salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            // تولید هش با PBKDF2
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password+ Pepper,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize
            );

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }


        // <summary>
        /// اعتبارسنجی رمز عبور با مقایسه رمز عبور ورودی با رمز عبور هش‌شده
        /// </summary>
        /// <param name="inputPassword">رمز عبور ورودی</param>
        /// <param name="hashedPassword">رمز عبور هش ‌شده ذخیره‌شده</param>
        /// <returns>در صورتی که رمز عبور صحیح باشد true، در غیر این صورت false</returns
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("رمز عبور نمی‌تواند خالی باشد", nameof(password));

            if (string.IsNullOrWhiteSpace(hashedPassword))
                throw new ArgumentException("رمز ذخیره‌شده معتبر نیست", nameof(hashedPassword));

            try
            {
                var parts = hashedPassword.Split('.', 2);
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] storedHash = Convert.FromBase64String(parts[1]);

                byte[] computedHash = KeyDerivation.Pbkdf2(
                    password: password+ Pepper,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: Iterations,
                    numBytesRequested: KeySize
                );

                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
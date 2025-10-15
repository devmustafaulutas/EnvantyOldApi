using envantyService.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace envantyService
{
    public interface ILoginService
    {
        /// <summary>
        /// Girilen userId veya e‑posta ve şifreyle kullanıcıyı doğrular.
        /// Başarılıysa Login entity’sini, değilse null döner.
        /// </summary>
        Login Authenticate(string userIdOrEmail, string password);

        /// <summary>Şifreyi hash’ler</summary>
        string HashPassword(string password);

        /// <summary>Şifreyi ve hash’i karşılaştırır</summary>
        bool VerifyPassword(string password, string passwordHash);
    }

    public class LoginService : ILoginService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<string> _passwordHasher;

        public LoginService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<string>();
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        public string HashPassword(string password)
            => _passwordHasher.HashPassword(null, password);

        public bool VerifyPassword(string password, string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success
                || result == PasswordVerificationResult.SuccessRehashNeeded;
        }

        public Login Authenticate(string userIdOrEmail, string password)
        {
            var user = IsValidEmail(userIdOrEmail)
                ? _context.Logins.FirstOrDefault(x => x.Email == userIdOrEmail)
                : _context.Logins.FirstOrDefault(x => x.UserId == userIdOrEmail);

            if (user == null)
                return null;

            return VerifyPassword(password, user.PasswordHash)
                ? user
                : null;
        }
    }
}

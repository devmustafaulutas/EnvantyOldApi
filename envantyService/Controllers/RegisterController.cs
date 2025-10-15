using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly AppDbContext _context;

        public RegisterController(AppDbContext context, ILoginService loginService)
        {
            _context = context;
            _loginService = loginService;
        }

        public class MobileRegisterRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] MobileRegisterRequest request)
        {
            // 1. E‑posta zaten var mı?
            if (_context.Logins.Any(u => u.Email == request.Email))
                return BadRequest(new { message = "Bu e‑posta zaten kayıtlı." });

            // 2. Şifreyi hash’le
            var passwordHash = _loginService.HashPassword(request.Password);

            // 3. Yeni kullanıcı oluştur (diğer alanları ihtiyacınıza göre doldurun)
            var user = new Login
            {
                UserId       = request.Email,
                Email        = request.Email,
                PasswordHash = passwordHash,
                Status       = true,
                FirstLogin   = true,
                CreateDate   = DateTime.UtcNow,
                RoleId       = 42,    // Örn: 2 = normal kullanıcı
                SirketlerId =  7

                // İsterseniz Name, Surname, SirketlerId vb. alanları de doldurabilirsiniz
            };

            _context.Logins.Add(user);
            _context.SaveChanges();

            // 4. Basit dönüş
            return Ok(new
            {
                user.Id,
                user.Email,
                message = "Kayıt başarılı."
            });
        }
    }
}

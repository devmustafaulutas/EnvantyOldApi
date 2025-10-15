using envantyService.Profil;
using envantyService.Sicil;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfilController(AppDbContext context)
        {
            _context = context;
        }

        //Password Değiştirme

        [HttpPost("password-code-verify")]
        public async Task<IActionResult> CodeVerify([FromBody] PSifreVerify model)
        {
            Console.WriteLine("E-posta: " + model.Email);
            Console.WriteLine("Doğrulama Kodu: " + model.VerificationCode);

            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.VerificationCode))
            {
                return BadRequest("E-posta ve doğrulama kodu gerekli.");
            }

            try
            {
                var kodkontrol = _context.PasswordManagement
                    .FirstOrDefault(x => x.Mail == model.Email && x.Kod == int.Parse(model.VerificationCode));

                if (kodkontrol != null)
                {
                    _context.PasswordManagement.Remove(kodkontrol);
                    await _context.SaveChangesAsync(); // Değişiklikleri kaydet
                    return Ok("Kod Doğrulandı");

                }
                else
                {
                    return BadRequest("Kod yanlış veya e-posta bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                // Hata loglama
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz." + ex.Message);
            }
        }

        [HttpPost("password-change")]
        public async Task<IActionResult> PasswordChange([FromBody] PSifre model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.NewPassword))
            {
                return BadRequest("E-posta ve yeni şifre gerekli.");
            }

            try
            {
                // E-posta ile kullanıcıyı kontrol et
                var mailcontrol = _context.Logins.Where(x => x.Email == model.Email).ToList();

                // Kullanıcı bulunamadıysa hata döndür
                if (!mailcontrol.Any())
                {
                    return BadRequest("Email bulunamadı!");
                }
                var passwordHasher = new PasswordHasher<string>();
                var hashedPassword = passwordHasher.HashPassword(null, model.NewPassword); // Şifreyi hashle
                // Şifreyi güncelle
                foreach (var item in mailcontrol)
                {
                    item.PasswordHash =hashedPassword;
                    item.FirstLogin = false;
                }

                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();

                return Ok("Şifre başarılı bir şekilde değiştirildi!");
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama ve kullanıcıya bilgi verme
                Console.WriteLine(ex.Message);
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz." + ex.Message);
            }
        }

        //Password Değiştirme Son

        //Mail Değiştirme 

        [HttpPost("mail-code-verify")]
        public async Task<IActionResult> MailCodeVerify([FromBody] PMailVerify model)
        {
            Console.WriteLine("Doğrulama Kodu: " + model.VerificationCode);
            if (string.IsNullOrWhiteSpace(model.VerificationCode))
            {
                return BadRequest(new { success = false, message = "Doğrulama kodu gerekli." });
            }

            try
            {
                var kodkontrol = _context.PasswordManagement
                    .FirstOrDefault(x => x.Mail == model.Mail && x.Kod == int.Parse(model.VerificationCode));

                if (kodkontrol != null)
                {
                    _context.PasswordManagement.Remove(kodkontrol);
                    await _context.SaveChangesAsync(); // Değişiklikleri kaydet
                    return Ok("Kod başarılı");

                }
                else
                {
                    return BadRequest("Kod yanlış veya hatalı");
                }
            }
            catch (Exception ex)
            {
                // Hata loglama
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz."+ex.Message);
            }
        }

        [HttpPost("mail-change")]
        public async Task<IActionResult> MailChange([FromBody] PMailChange model)
        {
            if (string.IsNullOrEmpty(model.CurrentMail) || string.IsNullOrEmpty(model.NewMail))
            {
                return BadRequest("E-posta ve yeni mail gerekli.");
            }

            try
            {
                // E-posta ile kullanıcıyı kontrol et
                var mailcontrol = _context.Logins.Where(x => x.Email == model.CurrentMail).ToList();

                // Kullanıcı bulunamadıysa hata döndür
                if (!mailcontrol.Any())
                {
                    return BadRequest("Email bulunamadı!");
                }

                foreach (var item in mailcontrol)
                {
                    item.Email =model.NewMail;
                    item.FirstLogin = false;
                }

                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();

                return Ok("Şifre başarılı bir şekilde değiştirildi!");
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama ve kullanıcıya bilgi verme
                Console.WriteLine(ex.Message);
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz." + ex.Message);
            }
        }
        [HttpPost("phone-change")]
        public async Task<IActionResult> PhoneChange([FromBody] PPhoneChange model)
        {
            if (string.IsNullOrEmpty(model.CurrentMail))
            {
                return BadRequest("E-posta ve yeni mail gerekli.");
            }

            try
            {
                // E-posta ile kullanıcıyı kontrol et
                var mailcontrol = _context.Logins.Where(x => x.Email == model.CurrentMail).ToList();

                // Kullanıcı bulunamadıysa hata döndür
                if (!mailcontrol.Any())
                {
                    return BadRequest("Email bulunamadı!");
                }

                foreach (var item in mailcontrol)
                {
                    item.PhoneNumber = model.NewPhoneNumber;
                }

                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();

                return Ok("Telefon başarılı bir şekilde güncellendi!");
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama ve kullanıcıya bilgi verme
                Console.WriteLine(ex.Message);
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz." + ex.Message);
            }
        }
        //Mail Değiştirme Son

        //Kullanıcı Silme
        [HttpPost("profil-delete")]
        public async Task<IActionResult> UserDelete(string? UserId)
        {
            var login = _context.Logins.Where(x => x.UserId == UserId).ToList();
            if (login.Any())
            {
                foreach (var item in login)
                {
                    item.Status = false;
                }
                _context.SaveChanges();
                return Ok("Kullanıcı silme başarılı");
            }
            else
            {
                return BadRequest("Kullanıcı silme başarısız");
            }
        }
        //Kullanıcı Silme Son

        //Kullanıcı Sicil Numarası

        [HttpPost("sicil-mail-change")]
        public async Task<IActionResult> SicilMailChange([FromBody] SMailChange model)
        {
            if (string.IsNullOrEmpty(model.SicilNo) || string.IsNullOrEmpty(model.NewMail))
            {
                return BadRequest("Sicil No ve yeni mail gerekli.");
            }

            try
            {
                // E-posta ile kullanıcıyı kontrol et
                var mailcontrol = _context.Logins.Where(x => x.UserId == model.SicilNo).ToList();

                // Kullanıcı bulunamadıysa hata döndür
                if (!mailcontrol.Any())
                {
                    return BadRequest("Kişi bulunamadı!");
                }

                foreach (var item in mailcontrol)
                {
                    item.Email =model.NewMail;
                }

                // Değişiklikleri veritabanına kaydet
                await _context.SaveChangesAsync();

                return Ok("Mail başarılı bir şekilde değiştirildi!");
            }
            catch (Exception ex)
            {
                // Hata durumunda loglama ve kullanıcıya bilgi verme
                Console.WriteLine(ex.Message);
                return BadRequest("Bir hata oluştu. Lütfen tekrar deneyiniz." + ex.Message);
            }
        }

        //Kullanıcı Sicil Numarası Son
    }
}

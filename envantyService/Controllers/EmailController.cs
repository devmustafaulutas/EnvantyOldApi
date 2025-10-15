using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using envantyefe.Entityframeworks;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly EmailSettings _emailSettings;
        private readonly AppDbContext _context;
        public EmailController(IOptions<EmailSettings> emailSettings , AppDbContext context)
        {
            _emailSettings = emailSettings.Value;
            _context = context;
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            try
            {
                await SendEmailAsync(request.RecipientEmail, request.Subject, request.Body);
                return Ok("Email sent successfully.");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }

        //[HttpPost("send-human")]
        //public async Task<IActionResult> SendEmailHuman([FromBody] EmailHumanRequest request)
        //{
        //    try
        //    {
        //        await SendEmailHumanAsync(request.Subject, request.Body);
        //        return Ok("Email sent successfully to human.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest($"Error sending email: {ex.Message}");
        //    }
        //}
        //[HttpPost("send-mobil")]
        //public async Task<IActionResult> SendEmailMobil([FromBody] EmailMobilRequest request)
        //{
        //    try
        //    {
        //        await SendEmailHumanAsync(request.RecipientEmail, request.Title);
        //        return Ok("Email sent successfully to human.");
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest($"Error sending email: {ex.Message}");
        //    }
        //}
        [HttpPost("send-password-code")]
        public async Task<IActionResult> SendPasswordCode(string RecipientEmail)
        {
            var kontrol = _context.Logins.FirstOrDefault(x=>x.Email == RecipientEmail);
            if(kontrol == null)
            {
                return BadRequest("Geçersiz Email");
            }
            var random = new Random();
            int randomNumber = random.Next(100000, 1000000); // 100000 ile 999999 arası 
            var kod = new Sifre()
            {
                CreateDate = DateTime.Now,
                Kod = randomNumber,
                Mail = RecipientEmail
            };
            _context.PasswordManagement.Add(kod);
            _context.SaveChanges();
            string body = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            margin: 0;
                                            padding: 0;
                                            background-color: #f4f4f4;
                                            color: #333;
                                        }}
                                        .container {{
                                            max-width: 600px;
                                            margin: 20px auto;
                                            background-color: #ffffff;
                                            border-radius: 8px;
                                            overflow: hidden;
                                            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                                        }}
                                        .header {{
                                            background-color: #18274a;
                                            color: white;
                                            padding: 20px;
                                            text-align: center;
                                            font-size: 24px;
                                            font-weight: bold;
                                        }}
                                        .content {{
                                            padding: 20px;
                                            font-size: 16px;
                                            line-height: 1.5;
                                            color: #555555;
                                        }}
                                        .content p {{
                                            margin: 0 0 15px;
                                        }}
                                        .button {{
                                            display: block;
                                            text-align: center;
                                            margin: 20px 0;
                                        }}
                                        .button a {{
                                            background-color: #ff6600;
                                            color: white;
                                            text-decoration: none;
                                            padding: 12px 20px;
                                            border-radius: 5px;
                                            font-size: 16px;
                                        }}
                                        .button a:hover {{
                                            background-color: #e65c00;
                                        }}
                                        .footer {{
                                            text-align: center;
                                            padding: 15px;
                                            font-size: 12px;
                                            color: #777777;
                                            background-color: #f4f4f4;
                                        }}
                                        @media only screen and (max-width: 600px) {{
                                            .container {{
                                                padding: 15px;
                                            }}
                                            .content {{
                                                font-size: 14px;
                                            }}
                                            .header {{
                                                font-size: 20px;
                                            }}
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <div class='header'>GETİNGE - Parola Sıfırlama</div>
                                        <div class='content'>
                                            <p>Merhaba,</p>
                                            <p>Parolanızı sıfırlamak için aşağıdaki kodu kullanabilirsiniz:</p>
                                            <p style='font-size: 20px; font-weight: bold; text-align: center; color: #18274a;'>{randomNumber}</p>
                                            <p>Eğer bu isteği siz yapmadıysanız, lütfen bu mesajı dikkate almayınız.</p>
                                            <p>Teşekkürler,<br/>Getinge Ekibi</p>
                                        </div>
                                        <div class='footer'>
                                            Bu mesaj otomatik olarak oluşturulmuştur, lütfen yanıtlamayınız.
                                        </div>
                                    </div>
                                </body>
                                </html>";
            var subject = "GETİNGE - Şifre Değiştirme";
            try
            {
                await SendEmailAsync(RecipientEmail, subject , body);
                return Ok("Email sent successfully to code");
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }

        [HttpPost("send-mail-code")]
        public async Task<IActionResult> SendMailChangeCode(string mail)
        {
            Console.WriteLine("Mail received: " + mail);  // Verinin doğru alınıp alınmadığını kontrol etmek için

            if (string.IsNullOrEmpty(mail))
            {
                return BadRequest("Mail parametresi boş!");
            }

            var komail = _context.Logins.FirstOrDefault(x => x.Email == mail);
            if (komail == null)
            {
                return BadRequest("Geçersiz mail adresi girdiniz!");
            }
            var random = new Random();
            int randomNumber = random.Next(100000, 1000000); // 100000 ile 999999 arası 

            var mailkod = new Sifre()
            {
                Kod = randomNumber,
                Mail = mail,
                CreateDate = DateTime.Now,
            };
            try
            {
                _context.PasswordManagement.Add(mailkod);
                _context.SaveChanges();

                try
                {
                    string recipientEmail = mail; // Alıcı e-posta adresi
                    string subject = "GETİNGE - E-Mail Değiştirme";
                    string body = $@"
                                <html>
                                <head>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            margin: 0;
                                            padding: 0;
                                            background-color: #f4f4f4;
                                            color: #333;
                                        }}
                                        .container {{
                                            max-width: 600px;
                                            margin: 20px auto;
                                            background-color: #ffffff;
                                            border-radius: 8px;
                                            overflow: hidden;
                                            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
                                        }}
                                        .header {{
                                            background-color: #18274a;
                                            color: white;
                                            padding: 20px;
                                            text-align: center;
                                            font-size: 24px;
                                            font-weight: bold;
                                        }}
                                        .content {{
                                            padding: 20px;
                                            font-size: 16px;
                                            line-height: 1.5;
                                            color: #555555;
                                        }}
                                        .content p {{
                                            margin: 0 0 15px;
                                        }}
                                        .button {{
                                            display: block;
                                            text-align: center;
                                            margin: 20px 0;
                                        }}
                                        .button a {{
                                            background-color: #ff6600;
                                            color: white;
                                            text-decoration: none;
                                            padding: 12px 20px;
                                            border-radius: 5px;
                                            font-size: 16px;
                                        }}
                                        .button a:hover {{
                                            background-color: #e65c00;
                                        }}
                                        .footer {{
                                            text-align: center;
                                            padding: 15px;
                                            font-size: 12px;
                                            color: #777777;
                                            background-color: #f4f4f4;
                                        }}
                                        @media only screen and (max-width: 600px) {{
                                            .container {{
                                                padding: 15px;
                                            }}
                                            .content {{
                                                font-size: 14px;
                                            }}
                                            .header {{
                                                font-size: 20px;
                                            }}
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class='container'>
                                        <div class='header'>GETİNGE - E-Mail Sıfırlama</div>
                                        <div class='content'>
                                            <p>Merhaba,</p>
                                            <p>E-mailinizi sıfırlamak için aşağıdaki kodu kullanabilirsiniz:</p>
                                            <p style='font-size: 20px; font-weight: bold; text-align: center; color: #18274a;'>{randomNumber}</p>
                                            <p>Eğer bu isteği siz yapmadıysanız, lütfen bu mesajı dikkate almayınız.</p>
                                            <p>Teşekkürler,<br/>Getinge Ekibi</p>
                                        </div>
                                        <div class='footer'>
                                            Bu mesaj otomatik olarak oluşturulmuştur, lütfen yanıtlamayınız.
                                        </div>
                                    </div>
                                </body>
                                </html>";

                    await SendEmailAsync(recipientEmail, subject, body);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"E-posta gönderimi başarısız: {e.Message}");
                }

                return Ok("E-Mail başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                // Yalnızca gerekli bilgileri JSON formatında döndür
                var errorMessage = new
                {
                    success = false,
                    message = ex.Message, // Hata mesajını al
                    stackTrace = ex.StackTrace // Stack trace'i de ekleyebilirsiniz, isteğe bağlı
                };

                return BadRequest(errorMessage); // JSON yanıtı döndür
            }
        }

        private async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, "GETİNGE PORTAL"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(recipientEmail);

            using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
            {
                smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
                smtpClient.EnableSsl = _emailSettings.EnableSsl;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        //private async Task SendEmailHumanAsync(string subject, string body)
        //{
        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_emailSettings.SenderEmail, "GETİNGE PORTAL"),
        //        Subject = subject,
        //        Body = body,
        //        IsBodyHtml = true
        //    };
        //    mailMessage.To.Add("esra.oskaner@getinge.com");

        //    using (var smtpClient = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port))
        //    {
        //        smtpClient.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
        //        smtpClient.EnableSsl = _emailSettings.EnableSsl;
        //        await smtpClient.SendMailAsync(mailMessage);
        //    }
        //}
    }
}

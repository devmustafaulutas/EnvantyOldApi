using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BilgiController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EmailController _emailService;
        public BilgiController(AppDbContext context, EmailController emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> BilgiTalepCevap(string Cevap , string? Mail , int TakipNumarisi)
        {
            var numara = _context.TakipForms.FirstOrDefault(x => x.TakipNumarısı == TakipNumarisi);
            
            if (string.IsNullOrEmpty(Mail))
            {
                var aksiyon = new ActionHistory()
                {
                    AksiyonAdı = "Bilgi Talepe Cevap",
                    AksiyonAçıklaması = Cevap,
                    AksiyonYapanKişi = "",
                    TakipFormId = numara.TakipNumarısı,
                    YapılmaTarihi = DateTime.Now,
                };
                _context.ActionHistory.Add(aksiyon);
            }
            else
            {
                var mail = Mail;
                var mails = _context.Logins.FirstOrDefault(x => x.Email == mail);
                var aksiyon = new ActionHistory()
                {
                    AksiyonAdı = "Bilgi Talepe Cevap",
                    AksiyonAçıklaması = Cevap,
                    AksiyonYapanKişi = mails.Name +" " + mails.Surname,
                    TakipFormId = numara.TakipNumarısı,
                    YapılmaTarihi = DateTime.Now,
                };
                _context.ActionHistory.Add(aksiyon);
            }


            if (numara != null)
            {
                numara.Status = "Açık";
                try
                {
                    try
                    {
                        var atanakisi = _context.Logins.FirstOrDefault(x => x.Email == numara.SorumluMail);
                        string nameSurname = numara.NameSurname == "İsim verilmedi" ? "GETINGE GETINGE" : numara.NameSurname;
                        var departname = _context.Departmans.FirstOrDefault(x => x.Id == numara.DepartmanId);
                        var departcategoryname = _context.DepartmanCategories.FirstOrDefault(x => x.Id == numara.DepartmanCategoryId);
                        string recipientEmail = numara.SorumluMail; // Alıcı e-posta adresi
                        string subject = numara.TakipNumarısı + " takip numaralı Çalışan İletişim Platformu (ÇİP) kategorisinden açılan kayıttan bilgi talebe cevap verilmiştir.";
                        string body = $@"
                                    <html>
                                    <head>
                                        <style>
                                            .header {{
                                                background-color: #0C6023;
                                                color: white;
                                                font-weight: bold;
                                                padding: 10px;
                                                text-align: left;
                                                font-family: Arial, sans-serif;
                                                max-width:650px;
                                                font-size: 20px;
                                            }}
                                            .content {{
                                                font-family: Arial, sans-serif;
                                                margin: 10px;
                                                font-size: 15px;
                                            }}
                                            .row {{
                                                display: flex;
                                                margin: 10px 0;
                                            }}
                                            .row .label {{
                                                width: 220px; /* Sabit genişlik */
                                                font-weight: bold;
                                                color: #18274a;
                                                font-size: 17px;
                                            }}
                                            .row .value {{
                                                flex: 1;
                                                font-size: 15px;
                                                color : black;
                                            }}
                                        </style>
                                    </head>
                                    <body>
                                        <div class='header'>Öneri ve İyileştirme - Çalışan İletişim Platformu (ÇİP) kategorisinden açılan kayıttan bilgi talebe cevap verilmiştir.</div>
                                        <div class='content'>
                                            <div class='row'><div class='label'>Kayıt Açılış Tarihi:</div><div class='value'>{numara.CreateDate:dd/MM/yyyy HH:mm}</div></div>
                                            <div class='row'><div class='label'>Kaydı Açan Kişi:</div><div class='value'>{nameSurname}</div></div>
                                            <div class='row'><div class='label'>Kayıt Statüsü:</div><div class='value'>Bilgi Talep Et</div></div>
                                            <div class='row'><div class='label'>Yapılan Aktivite:</div><div class='value'>Bilgi Talep Et</div></div>
                                            <div class='row'><div class='label'>Yapılan Aktivite Açıklaması:</div><div class='value'>{Cevap}</div></div>
                                            <div class='row'><div class='label'>Uygulama:</div><div class='value'>Öneri ve İyileştirme</div></div>
                                            <div class='row'><div class='label'>Kayıt Tipi:</div><div class='value'>Çalışan İletişim Platformu</div></div>
                                            <div class='row'><div class='label'>Takip Numarası:</div><div class='value'>{numara.TakipNumarısı}</div></div>
                                            <div class='row'><div class='label'>Ad Soyad:</div><div class='value'>{nameSurname}</div></div>
                                            <div class='row'><div class='label'>E-Posta Adresi:</div><div class='value'>{numara.Email}</div></div>
                                            <div class='row'><div class='label'>İlgili Departman:</div><div class='value'>{departname.Description}</div></div>
                                            <div class='row'><div class='label'>Kategori:</div><div class='value'>{departcategoryname.Name}</div></div>
                                            <div class='row'><div class='label'>Başlık:</div><div class='value'>{numara.Title}</div></div>
                                            <div class='row'><div class='label'>İçerik:</div><div class='value'>{numara.İçerik}</div></div>
                                            <div class='row'><div class='label'>Atanan Kişi:</div><div class='value'>{atanakisi.Name + " " + atanakisi.Surname}</div></div>
                                            <div class='row'><div class='label'>Atanan Grup:</div><div class='value'></div></div>
                                        </div>
                                    </body>
                                    </html>";
                        var email = new EmailRequest()
                        {
                            Body = body,
                            RecipientEmail = recipientEmail,
                            Subject = subject,
                        };
                        await _emailService.SendEmail(email);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"E-posta gönderimi başarısız: {e.Message}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                _context.SaveChanges();
            }
            else
            {
                return Json(new { success = false, message = "İşlem Başarısız!" });
            }
            return Json(new { success = true, message = "Bilgi Talepe Cevap Verme işlemi başarıyla gerçekleşti" });
        }
    }
}

using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Tüm metotlar için kimlik doğrulama gerekti
    public class AyınProjesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AyınProjesController(AppDbContext context)
        {
            _context = context;

        }

     

        [HttpGet("GetByTakipNumarisi")]
        public async Task<ActionResult> GetByTakipNumarisi([FromQuery] int takipNumarisi)
        {
            // Veritabanında LEFT JOIN ile AyınProjes ve adminCevaplamas kayıtlarını birleştiriyoruz
            var result = await (from ap in _context.AyınProjes
                                join ac in _context.adminCevaplamas
                                on ap.TakipNumarisi equals ac.TakipFormId into cevaplamaJoin
                                from ac in cevaplamaJoin.DefaultIfEmpty()
                                where ap.TakipNumarisi == takipNumarisi
                                select new
                                {
                                    ProjeId = ap.Id,
                                    TakipNumarisi = ap.TakipNumarisi,
                                    NameSurname = ap.NameSurname,
                                    Email = ap.Email,
                                    ProjeBasligi = ap.Title,
                                    ProjeOlusturmaTarihi = ap.CreatedDate,
                                    CevapId = ac != null ? ac.Id : (int?)null,
                                    CevaplayanAdmin = ac != null ? ac.AdminName : null,
                                    CevapMetni = ac != null ? ac.CevapIcerigi : null,
                                    CevapTarihi = ac != null ? ac.CevapTarihi : (DateTime?)null
                                }).ToListAsync();

            // Eğer kayıt bulunamazsa 404 döndür
            if (result == null || !result.Any())
            {
                return NotFound($"No data found with TakipNumarisi: {takipNumarisi}");
            }

            // Kayıtları döndür
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<AyınProjes>> PostAyınProjes(AyınProjes ayınProjes)
        {
            // Oluşturma tarihini ve takip numarasını ayarla
            ayınProjes.CreatedDate = DateTime.Now;
            ayınProjes.TakipNumarisi = await GenerateUniqueTrackingNumberAsync();

            
            // Create a new TakipForm instance
            var takipForm = new TakipForm
            {
                TakipNumarısı = ayınProjes.TakipNumarisi,
                NameSurname = ayınProjes.NameSurname, // CIP'den TakipForm'a uygun alanları eşleştir
                Email = ayınProjes.Email,
                Title = ayınProjes.Title,
                İçerik=ayınProjes.Icerik,
                CreateDate = DateTime.Now,
                Status = "Açık", // Varsayılan bir durum değeri verilebilir
                SorumluMail = "", // DepartmanCategory'den Mail alınır
                DepartmanCategoryId = 0,
                KayitTipi = "Ayın Projesi",
                DocumentPath = ayınProjes.DocumentPath,
                DepartmanId=5,
                ShowMail=false,
                AyınProjeKonu=ayınProjes.Konu
            };
            // Yeni projeyi veritabanına ekle
            _context.AyınProjes.Add(ayınProjes);
            _context.TakipForms.Add(takipForm);
            await _context.SaveChangesAsync();

            // Takip numarası üzerinden GET metodu için CreatedAtAction kullan
            return CreatedAtAction(
                "GetByTakipNumarisi",
                new { takipNumarisi = ayınProjes.TakipNumarisi },
                ayınProjes
            );
        }


        private async Task<int> GenerateUniqueTrackingNumberAsync()
        {
            int trackingNumber;
            bool exists;

            do
            {
                trackingNumber = new Random().Next(100000000, 999999999);
                exists = await _context.AyınProjes.AnyAsync(c => c.TakipNumarisi == trackingNumber);
            } while (exists);

            return trackingNumber;
        }

        [HttpGet("GetProjeKonusu")]
        public async Task<ActionResult<IEnumerable<AyinProjeKonus>>> GetAyinProjeKonus()
        {
            return await _context.AyınProjeKonus.ToListAsync();
        }
    }
}

using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OmusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OmusController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetOmus")]
        public async Task<ActionResult<IEnumerable<OmusWithDetailsDto>>> GetOmusDetails()
        {
            var result = await (from o in _context.Omus
                                join ow in _context.OmuAnswer on o.TakipNumarisi equals ow.OmuId into answers
                                from answer in answers.DefaultIfEmpty()
                                join od in _context.Omu_Departman on o.omu_DepartmanId equals od.Id into departments
                                from department in departments.DefaultIfEmpty()
                                join rhGroup in _context.RegistrationHistory
                                    .Where(x => x.AksiyonAdı == "Kapat")
                                    on o.TakipNumarisi equals rhGroup.TakipFormId into rhKapat
                                from rh in rhKapat.DefaultIfEmpty()
                                where o.Status != "false"
                                select new OmusWithDetailsDto
                                {
                                    Id = o.Id,
                                    NameSurname = o.NameSurname,
                                    Email = o.Email,
                                    DepartmanName = department.Name,
                                    Planla_Acıklaması = answer != null ? answer.Planla_Acıklaması : null,
                                    Uygula_Acıklaması = answer != null ? answer.Uygula_Acıklaması : null,
                                    Kontrol_Et_Acıklaması = answer != null ? answer.Kontrol_Et_Acıklaması : null,
                                    Onlem_Al_Acıklaması = answer != null ? answer.Onem_Al_Acıklaması : null,
                                    Description = o.Description,
                                    DocumentPath = o.DocumentPath,
                                    TakipNumarasi = o.TakipNumarisi,
                                    DepartmanId = o.DepartmanId,
                                    CreateDate = o.CreateDate,
                                    SorumluKisi = o.SorumluKisi,
                                    omu_DepartmanId = o.omu_DepartmanId,
                                    IyilestirmetipiId = o.IyileştirmetipiId,
                                    Status = o.Status,
                                    KapanKisi=rh.AksiyonYapanKisi,
                                    Iyilestirme_Degerlendirmesi = o.Iyilestirme_Degerlendirmesi,
                                    CloseDate = rh != null ? rh.OlusturulmaZamanı : null
                                }).ToListAsync();


            return Ok(result);
        }

        [HttpGet("GetOmusDepartment")]
        public List<Omu_Departman> GetOmusDepartmanlar()
        {
            return _context.Omu_Departman.ToList();
        }

        [HttpGet("GetOmusIyilestirmeTipi")]
        public List<IyilestirmeTipi> GetIyilestirmeTipi()
        {
            return _context.Iyileştirmetipis.ToList();
        }

        [HttpGet("GetOmusAndAnswerById/{omuId}")]
        public async Task<ActionResult<OmusWithDetailsDto>> GetOmusAndAnswerById(int omuId)
        {
            var result = await (from o in _context.Omus
                                join ow in _context.OmuAnswer on o.TakipNumarisi equals ow.OmuId into answers
                                from answer in answers.DefaultIfEmpty()
                                join od in _context.Omu_Departman on o.omu_DepartmanId equals od.Id into departments
                                from department in departments.DefaultIfEmpty()
                                where o.Status != "false" && o.TakipNumarisi == omuId // Status değeri "false" olanları dışla ve verilen ID'ye göre filtrele
                                select new OmusWithDetailsDto
                                {
                                    Id = o.Id,
                                    NameSurname = o.NameSurname,
                                    Email = o.Email,
                                    DepartmanName = department.Name,
                                    Planla_Acıklaması = answer != null ? answer.Planla_Acıklaması : null,
                                    Uygula_Acıklaması = answer != null ? answer.Uygula_Acıklaması : null,
                                    Kontrol_Et_Acıklaması = answer != null ? answer.Kontrol_Et_Acıklaması : null,
                                    Onlem_Al_Acıklaması = answer != null ? answer.Onem_Al_Acıklaması : null,
                                    Description = o.Description,
                                    DocumentPath = o.DocumentPath,
                                    TakipNumarasi = o.TakipNumarisi,
                                    DepartmanId = o.DepartmanId,
                                    CreateDate = o.CreateDate,
                                    SorumluKisi = o.SorumluKisi,
                                    omu_DepartmanId = o.omu_DepartmanId,
                                    IyilestirmetipiId = o.IyileştirmetipiId,
                                    Status = o.Status,
                                    Iyilestirme_Degerlendirmesi = o.Iyilestirme_Degerlendirmesi
                                }).FirstOrDefaultAsync(); // İlk eşleşeni al

            if (result == null)
            {
                return NotFound(); // Eğer sonuç yoksa 404 döndür
            }

            return Ok(result); // Sonucu döndür
        }

        [HttpPost]
        public async Task<IActionResult> CreateOmus([FromBody] Omus omus)
        {
            if (omus == null)
            {
                return BadRequest("Geçersiz istek.");
            }

            // Formdan gelen veride status'u her zaman true olacak şekilde ayarla
            omus.Status = "Açık";

            // KayitTipi'ni çıkartıyoruz
            omus.KayitTipi = null;

            // Takip numarasını oluştur
            omus.TakipNumarisi = GenerateUniqueTrackingNumberAsync();

            // Diğer nullable alanları varsayılan değerleriyle ayarlayabiliriz
            omus.SorumluKisi = null;
            omus.Iyilestirme_Degerlendirmesi = null;

            var omusToSave = new Omus
            {
                NameSurname = omus.NameSurname,
                Email = omus.Email,
                Description = omus.Description,
                DocumentPath = omus.DocumentPath,
                TakipNumarisi = omus.TakipNumarisi,
                omu_DepartmanId = omus.omu_DepartmanId,
                IyileştirmetipiId = omus.IyileştirmetipiId,
                Status = omus.Status,
                DepartmanId = omus.DepartmanId,
                Iyilestirme_Degerlendirmesi = omus.Iyilestirme_Degerlendirmesi,
                CreateDate = DateTime.Now,
                SorumluKisi = omus.SorumluKisi
            };

            try
            {
                // Veritabanına ekle
                _context.Omus.Add(omusToSave);

                // Veritabanına kaydet
                await _context.SaveChangesAsync();

                // Başarılı yanıt döndür
                return CreatedAtAction(nameof(CreateOmus), new { id = omusToSave.Id }, new { message = "Omus başarıyla oluşturuldu.", TakipNumarasi = omusToSave.TakipNumarisi });
            }
            catch (Exception ex)
            {
                // Hata mesajını döndür
                return StatusCode(500, $"Kayıt sırasında bir hata oluştu: {ex.InnerException.Message}");
            }
        }

        private int GenerateUniqueTrackingNumberAsync()
        {
            int trackingNumber;
            bool exists;

            do
            {
                trackingNumber = new Random().Next(100000000, 999999999);
                exists = _context.CIPs.Any(c => c.TakipNumarısı == trackingNumber);
            } while (exists);

            return trackingNumber;
        }
    }
}

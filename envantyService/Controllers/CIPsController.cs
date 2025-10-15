
using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CIPController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CIPController(AppDbContext context)
        {
            _context = context;
        }



        //[HttpPost]
        //public async Task<IActionResult> CreateCIP([FromBody] CIP cip)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // Generate unique tracking number
        //    cip.TakipNumarısı = await GenerateUniqueTrackingNumberAsync();

        //    _context.CIPs.Add(cip);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCIP), new { trackingNumber = cip.TakipNumarısı }, cip);

        //}
        [HttpPost]
        public async Task<IActionResult> CreateCIP([FromBody] CIP cip,bool vShowMail)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generate unique tracking number
            cip.TakipNumarısı = await GenerateUniqueTrackingNumberAsync();

            // Fetch the DepartmanCategory using DepartmanCategoryId
            var departmanCategory = await _context.DepartmanCategories
                                                  .FirstOrDefaultAsync(dc => dc.Id == cip.DepartmanCategoryId);
            if (departmanCategory == null)
                return BadRequest("Invalid DepartmanCategoryId");

            // Create a new TakipForm instance
            var takipForm = new TakipForm
            {
                TakipNumarısı = cip.TakipNumarısı,
                NameSurname = cip.NameSurname, // CIP'den TakipForm'a uygun alanları eşleştir
                Email = cip.Eposta,
                Title = cip.Title,
                İçerik = cip.Content, // İçerik alanını doğru eşleştirin
                CreateDate = DateTime.Now,
                Status = "Açık", // Varsayılan bir durum değeri verilebilir
                SorumluMail = departmanCategory.Mail, // DepartmanCategory'den Mail alınır
                DepartmanCategoryId = cip.DepartmanCategoryId,
                KayitTipi = "Çalışan İletişim Platformu",
                DocumentPath = cip.DocumentPath,
                DepartmanId = cip.DepartmanId,
                ShowMail = vShowMail
            };

            // Add both CIP and TakipForm to the context
            cip.Status = true;
            _context.CIPs.Add(cip);
            _context.TakipForms.Add(takipForm);

            // Save changes
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCIP), new { trackingNumber = cip.TakipNumarısı }, cip);
        }



        [HttpGet("{trackingNumber}")]
        public async Task<IActionResult> GetCIP(int trackingNumber)
        {
            var cip = await _context.TakipForms.FirstOrDefaultAsync(c => c.TakipNumarısı == trackingNumber);

            if (cip == null)
                return NotFound();

            return Ok(cip);
        }

        private async Task<int> GenerateUniqueTrackingNumberAsync()
        {
            int trackingNumber;
            bool exists;

            do
            {
                trackingNumber = new Random().Next(100000000, 999999999);
                exists = await _context.CIPs.AnyAsync(c => c.TakipNumarısı == trackingNumber);
            } while (exists);

            return trackingNumber;
        }
    }
}

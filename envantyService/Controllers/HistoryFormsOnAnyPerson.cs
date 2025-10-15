
using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryFormsOnAnyPerson : Controller
    {
        private readonly AppDbContext _context;
        public HistoryFormsOnAnyPerson(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Omus>>> GetMySending(string email)
        {
            var omusList = await _context.Omus
                .Where(o => o.Email == email) // Email filtresi
                .Select(o => new Omus
                {
                    Id = o.Id,
                    NameSurname = o.NameSurname,
                    Email = o.Email,
                    Description = o.Description,
                    DocumentPath = o.DocumentPath,
                    TakipNumarisi = o.TakipNumarisi,
                    omu_DepartmanId = o.omu_DepartmanId,
                    IyileştirmetipiId = o.IyileştirmetipiId,
                    Status = o.Status,
                    DepartmanId = o.DepartmanId,
                    Iyilestirme_Degerlendirmesi = o.Iyilestirme_Degerlendirmesi,
                    CreateDate = o.CreateDate,
                    SorumluKisi = o.SorumluKisi,
                    KayitTipi = "Omu" // Varsayılan değer
                })
                .Union(_context.TakipForms
                .Where(t => t.Email == email) // Email filtresi
                .Select(t => new Omus
                {
                    Id = t.Id, // TakipForms tablosunda ID olmadığı için varsayılan değer
                    NameSurname = t.NameSurname,
                    Email = t.Email,
                    Description = t.İçerik,
                    DocumentPath = t.DocumentPath,
                    TakipNumarisi = t.TakipNumarısı.Value,
                    omu_DepartmanId = 0,
                    IyileştirmetipiId = 0,
                    Status = t.Status,
                    DepartmanId = t.DepartmanId,
                    Iyilestirme_Degerlendirmesi = null,
                    CreateDate = t.CreateDate,
                    SorumluKisi = t.SorumluMail,
                    KayitTipi = t.KayitTipi // TakipForms'taki KayitTipi
                }))
                .OrderByDescending(omus => omus.CreateDate) // Tarihe göre sıralama
                .ToListAsync();

            return Ok(omusList);
        }



    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using envantyService.Models;


namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BirthdayManagerController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Dependency Injection ile veritabanı context'ini alıyoruz
        public BirthdayManagerController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Bugün doğum günü olan kullanıcıları getirir.
        /// </summary>
        /// <returns>Bugünün ay ve gününde doğan kullanıcıların listesi</returns>
        [HttpGet("birthdays-today")]
        public IActionResult GetTodayBirthdays()
        {
            //var today = DateTime.Now;

            // Veritabanından ay ve gün kontrolü yaparak sadece Name ve Username dönecek şekilde kullanıcıları filtreliyoruz
            var birthdaysToday = _context.Logins
                .Where(u => u.Birthdate.Day == DateTime.Today.Day && u.Birthdate.Month == DateTime.Today.Month)
                .GroupJoin(
                    _context.Departmans,
                    lo => lo.DepartmanId,    // Logins.DepartmanId
                    dp => dp.Id,            // Departmans.Id
                    (lo, dp) => new { lo, dp }) // Her login için departmanları eşleştir
                .SelectMany(
                    x => x.dp.DefaultIfEmpty(), // Eğer departman yoksa null döner
                    (x, dp) => new
                    {
                        x.lo.Name,
                        x.lo.Surname,
                        DepartmentAdi = dp != null ? dp.Description : null // Departman yoksa null
                    })
                .ToList();

            if (!birthdaysToday.Any())
                return NoContent();  // Eğer bugün doğan kimse yoksa 204 No Content döner

            return Ok(birthdaysToday);
        }
    }
}


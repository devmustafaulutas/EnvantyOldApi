using envantyefe.Entityframeworks;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DoctorController(AppDbContext context)
        {
            _context=context;
        }

        [HttpGet("get-doctor")]
        public async Task<IActionResult> GetDoctor()
        {
            var result = (from d in _context.Doctor
                         select new Doctor
                         {
                             Id = d.Id,
                             Day = d.Day,
                             DoctorShift = d.DoctorShift,
                             NurseShift= d.NurseShift,
                             CreateDate = d.CreateDate,
                             Status = d.Status
                         }).ToList();
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using envantyService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace envantyService.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrationHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationHistory>>> GetRegistrationHistories()
        {
            return await _context.RegistrationHistory.ToListAsync();
        }

        [HttpGet("{TakipFormId}")]
        public async Task<ActionResult<RegistrationHistory>> GetRegistrationHistory(int TakipFormId)
        {
            var registrationHistory = await _context.RegistrationHistory
                .FirstOrDefaultAsync(r => r.TakipFormId == TakipFormId);

            if (registrationHistory == null)
            {
                return NotFound();
            }

            return registrationHistory;
        }


        [HttpPost]
        public async Task<ActionResult<RegistrationHistory>> PostRegistrationHistory(RegistrationHistory registrationHistory)
        {
            _context.RegistrationHistory.Add(registrationHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistrationHistory), new { id = registrationHistory.Id }, registrationHistory);
        }
    }

}

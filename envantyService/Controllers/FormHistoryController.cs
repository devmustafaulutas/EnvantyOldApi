using Microsoft.AspNetCore.Mvc;
using Google;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using envantyService.Models;
namespace envantyService.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public class FormHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FormHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormHistory>>> GetFormHistories()
        {
            return await _context.FormHistory.ToListAsync();
        }

        [HttpGet("{TakipFormId}")]
        public async Task<ActionResult<FormHistory>> GetFormHistory(int TakipFormId)
        {
            var formHistory = await _context.FormHistory.FindAsync(TakipFormId);

            if (formHistory == null)
            {
                return NotFound();
            }

            return formHistory;
        }

        [HttpPost]
        public async Task<ActionResult<FormHistory>> PostFormHistory(FormHistory formHistory)
        {
            _context.FormHistory.Add(formHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFormHistory), new { id = formHistory.Id }, formHistory);
        }
    }

}

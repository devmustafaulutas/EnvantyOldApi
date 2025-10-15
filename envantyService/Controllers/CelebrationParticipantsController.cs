using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CelebrationParticipantsController : Controller
    {
        private readonly AppDbContext _context;

        public CelebrationParticipantsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("celebration/{celebrationId}")]
        public async Task<ActionResult<IEnumerable<CelebrationParticipants>>> GetParticipantsByCelebrationId(int celebrationId)
        {
            var participants = await _context.CelebrationParticipants
                .Where(p => p.CelebrationID == celebrationId)
                .ToListAsync();

            if (participants == null || !participants.Any())
            {
                return NotFound("No participants found for this celebration.");
            }

            return Ok(participants);
        }
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CelebrationParticipants>>> GetAllParticipants()
        {
            var participants = await _context.CelebrationParticipants.ToListAsync();

            if (participants == null || !participants.Any())
            {
                return NotFound("No participants found.");
            }

            return Ok(participants);
        }
        /// <summary>
        /// Inserts a new event participant with the current date and time.
        /// </summary>
        /// <param name="userMail">The email of the user.</param>
        /// <param name="eventId">The ID of the event.</param>
        /// <returns>Result of the insertion.</returns>
        [HttpPost("addcelebrationParticipant")]
        public async Task<IActionResult> AddParticipant(string userMail, int eventId)
        {
            var celebration = new CelebrationParticipants
            {
                UserMail = userMail,
                CreationDate = DateTime.Now,
                CelebrationID = eventId
            };

            _context.CelebrationParticipants.Add(celebration);

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Participant added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

    }
}

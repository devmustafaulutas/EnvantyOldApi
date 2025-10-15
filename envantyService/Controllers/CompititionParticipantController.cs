using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompititionParticipantController : Controller
    {
        private readonly AppDbContext _context;

        public CompititionParticipantController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllParticipants")]
        public async Task<IActionResult> GetAllParticipants()
        {
            try
            {
                var participants = await _context.CompetitionParticipants.ToListAsync();

                if (participants == null || !participants.Any())
                {
                    return NotFound("No participants found.");
                }

                return Ok(participants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpGet("getParticipantsByCompetitionId/{id}")]
        public async Task<IActionResult> GetParticipantsByCompetitionId(int id)
        {
            try
            {
                var participants = await _context.CompetitionParticipants
                    .Where(p => p.yarışmaYönetimiId == id)
                    .ToListAsync();

                if (participants == null || !participants.Any())
                {
                    return NotFound("No participants found for the given competition ID.");
                }

                return Ok(participants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        [HttpPost("addcompititionParticipant")]
        public async Task<IActionResult> AddParticipant(string userMail, int eventId)
        {
            var compitition = new CompititionParticipant
            {
                UserMail = userMail,
                CreationDate = DateTime.Now,
                yarışmaYönetimiId = eventId
            };

            _context.CompetitionParticipants.Add(compitition);

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

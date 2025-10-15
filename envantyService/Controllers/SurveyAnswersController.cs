using Microsoft.AspNetCore.Mvc;
using envantyService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using envantyService;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyAnswersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SurveyAnswersController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all SurveyAnswers.
        /// </summary>
        /// <returns>List of SurveyAnswers</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var surveyAnswers = _context.SurveyAnswers.ToList();
            return Ok(surveyAnswers);
        }

        /// <summary>
        /// Add a new SurveyAnswer to the database.
        /// </summary>
        /// <param name="request">SurveyAnswer model</param>
        /// <returns>Result message</returns>
        [HttpPost]
        public async Task<IActionResult> AddSurveyAnswer([FromBody] SurveyAnswer request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Invalid data." });
            }

            try
            {
                request.CreateDate = DateTime.Now; // Set CreateDate automatically
                request.Status = true;
                _context.SurveyAnswers.Add(request);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "SurveyAnswer added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred.", Error = ex.Message });
            }
        }

        /// <summary>
        /// Get a SurveyAnswer by UserName.
        /// </summary>
        /// <param name="userName">UserName of the survey answer</param>
        /// <returns>SurveyAnswer</returns>
        [HttpGet("{userName}")]
        public IActionResult GetSurveyAnswer(string userName)
        {
            var surveyAnswer = _context.SurveyAnswers
                .FirstOrDefault(sa => sa.UserName == userName);

            if (surveyAnswer == null)
            {
                return NotFound(new { Message = "SurveyAnswer not found." });
            }
            return Ok(surveyAnswer);
        }

        /// <summary>
        /// Get SurveyAnswers by SurveyNo.
        /// </summary>
        /// <param name="surveyNo">Survey number</param>
        /// <returns>List of SurveyAnswers</returns>
        [HttpGet("SurveyNo")]
        public IActionResult GetSurveyAnswersBySurveyNo(string surveyNo)
        {
            var surveyAnswers = _context.SurveyAnswers
                .Where(sa => sa.SurveyNo == surveyNo)
                .ToList();

            if (surveyAnswers == null || surveyAnswers.Count == 0)
            {
                return NotFound(new { Message = "SurveyAnswers not found." });
            }
            return Ok(surveyAnswers);
        }
    }
}

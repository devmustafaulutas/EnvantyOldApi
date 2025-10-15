using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using envantyService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using System.Text.Encodings.Web;

    namespace envantyService.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AnketYonetimiController : ControllerBase
        {
            private readonly AppDbContext _context;

            public AnketYonetimiController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet("GetBySurveyNo")]
            public async Task<IActionResult> GetBySurveyNo(string surveyNo)
            {
                if (string.IsNullOrEmpty(surveyNo))
                {
                    return BadRequest("SurveyNo is required.");
                }

                var anket = await _context.AnketYonetimi
                    .Where(a => a.SurveyNo == surveyNo)
                    .FirstOrDefaultAsync();

                if (anket == null)
                {
                    return NotFound("Anket not found.");
                }

                return Ok(anket);
            }

        [HttpGet("GetSurveysByEventId/{etkinlikId}")]
        public IActionResult GetSurveysByEventId(int etkinlikId)
        {
            var surveys = _context.AnketYonetimi
                .Where(a => a.EtkinlikId == etkinlikId)
                .Select(a => new
                {
                    a.SurveyNo,
                    a.SurveyMetaData
                })
                .ToList();

            if (surveys == null || !surveys.Any())
            {
                return NotFound();
            }

            return Ok(surveys);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnketYonetimi>>> AnketYonetimi(string? mail)
        {
            if (!string.IsNullOrEmpty(mail))
            {
                // Kullanıcının daha önce cevapladığı anketlerin SurveyNo'larını al
                var completedSurveyNos = await _context.SurveyAnswers
                    .Where(sa => sa.UserId == mail)
                    .Select(sa => sa.SurveyNo)  
                    .ToListAsync();

                // Kullanıcının henüz yanıtlamadığı ve etkin olan anketleri filtrele
                var anketYonetimiList = await _context.AnketYonetimi
                    .Where(a => a.Status == true
                                && a.EtkinlikId == 0
                                && !completedSurveyNos.Contains(a.SurveyNo)) // Önceki listeden filtreleme yapıldı
                    .OrderByDescending(a => a.CreateDate)
                    .ToListAsync();

                if (!anketYonetimiList.Any())
                {
                    return NotFound("Filtrelenmiş anket bulunamadı.");
                }

                return Ok(anketYonetimiList);
            }
            else
            {
                // Genel anket listesini getir (kullanıcıya özel değil)
                var anketYonetimiList = await _context.AnketYonetimi
                    .OrderByDescending(a => a.CreateDate)
                    .ToListAsync();

                if (!anketYonetimiList.Any())
                {
                    return NotFound("Filtrelenmiş anket bulunamadı.");
                }

                return Ok(anketYonetimiList);
            }
        }

        [HttpPut("Deactivate/{surveyNo}")]
            public async Task<IActionResult> DeactivateSurvey(string surveyNo)
            {
                if (string.IsNullOrEmpty(surveyNo))
                {
                    return BadRequest("SurveyNo is required.");
                }

                var anket = await _context.AnketYonetimi
                    .Where(a => a.SurveyNo == surveyNo)
                    .FirstOrDefaultAsync();

                if (anket == null)
                {
                    return NotFound("Anket not found.");
                }

                anket.Status = false;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Anket successfully deactivated." });
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            public IActionResult CreateAnket([FromBody] AnketYonetimi anket)
            {
                if (anket == null)
                {
                    return BadRequest(new { Message = "Anket cannot be null." });
                }

                try
                {
                    if (!string.IsNullOrEmpty(anket.SurveyMetaData))
                    {
                        try
                        {
                         

                            // JsonSerializerOptions ile encoder ayarı
                            var options = new JsonSerializerOptions
                            {
                                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                                WriteIndented = true // Daha okunabilir bir JSON çıktısı için
                            };

                            var surveyMetaData = JsonDocument.Parse(anket.SurveyMetaData);
                            anket.SurveyMetaData = JsonSerializer.Serialize(surveyMetaData,options);
                            var events = _context.Etkinliks.FirstOrDefault(x => x.Id == anket.EtkinlikId);
                            if(events != null)
                            {
                                events.Status = true;
                            }
                        }
                        catch (JsonException)
                        {
                            return BadRequest(new { Message = "Invalid JSON format in SurveyMetaData." });
                        }
                    }

                    _context.AnketYonetimi.Add(anket);
                    _context.SaveChanges();

                    return Ok(new
                    {
                        Message = "Record inserted successfully.",
                        Data = anket
                    });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new
                    {
                        Message = "An error occurred while saving the data.",
                        Details = ex.Message
                    });
                }
            }
        }
    }


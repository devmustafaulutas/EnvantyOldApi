using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;
using envantyService.Models;
namespace envantyService.Controllers
{
   

    [ApiController]
    [Route("api/[controller]")]
    public class TakipFormController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public TakipFormController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetTakipFormResponses")]
        public async Task<IActionResult> GetTakipFormResponses(int takipNumarisi)
        {
         
            var query = @"SELECT  tf.*, 
    ac.AksiyonAçıklaması AS CevapIcerigi,
    ac.YapılmaTarihi as CevapTarihi,tf.AyınProjeKonu
    FROM takipForms tf 
    left outer join [dbo].[ActionHistory] ac
    ON tf.TakipNumarısı = ac.takipFormId
    WHERE tf.TakipNumarısı = @TakipNumarisi and (ac.AksiyonAdı='Çözümle' OR ac.AksiyonAdı is null)";

            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                var result = await connection.QueryAsync<TakipFormResponse>(query, new { TakipNumarisi = takipNumarisi });

                if (!result.Any())
                {
                    return NotFound($"No records found for TakipNumarisi: {takipNumarisi}");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database connection failed: {ex.Message}");
            }
        }


    }

}

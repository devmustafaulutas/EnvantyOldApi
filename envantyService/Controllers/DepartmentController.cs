using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of department categories.
        /// </summary>
        /// <returns>List of DepartmentCategoryDTO</returns>
        [HttpGet("Categories")]
        public async Task<ActionResult<List<DepartmentCategoryDTO>>> GetDepartmentCategories()
        {
            var result = await (from dc in _context.DepartmanCategories
                                join dp in _context.Departmans on dc.DepartmanId equals dp.Id
                                select new DepartmentCategoryDTO
                                {
                                    DepartmanName = dp.Description,
                                    DepartmentId = dp.Id,
                                    CategoryName = dc.Name,
                                    CategoryId = dc.Id
                                }).ToListAsync();

            return Ok(result);
        }
    }

}

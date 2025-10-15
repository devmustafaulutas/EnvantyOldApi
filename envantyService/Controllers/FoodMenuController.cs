using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envantyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodMenuController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodMenuController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get food menu list based on the DateOfMeal.
        /// </summary>
        /// <param name="dateOfMeal">The date for which to retrieve the food menu.</param>
        /// <returns>A list of food menu items.</returns>
        [HttpGet]
        [Route("GetMenuByDate")]
        public async Task<ActionResult<IEnumerable<FoodMenuList>>> GetMenuByDate([FromQuery] DateTime dateOfMeal)
        {
            var menu = await _context.FoodMenuList
                                     .Where(m => m.DateOfMeal == dateOfMeal)
                                     .ToListAsync();

            if (menu == null || !menu.Any())
            {
                return NotFound("No menu found for the specified date.");
            }

            return Ok(menu);
        }
        [HttpGet]
        [Route("GetAllMenuByMonth")]
        public async Task<ActionResult<IEnumerable<FoodMenuList>>> GetAllMenu()
        {
            var menu = await _context.FoodMenuList
                                     .ToListAsync();

            if (menu == null || !menu.Any())
            {
                return NotFound("No menu found for the specified date.");
            }

            return Ok(menu);
        }
    }
}

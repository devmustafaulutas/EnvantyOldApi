using envantyService;
using envantyService.Models;
using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class UserTokensController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserTokensController(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Get all user tokens
    /// </summary>
    /// <returns>List of UserTokens</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserToken>>> Get()
    {
        var userTokens = await _context.UserTokens.ToListAsync();
        return Ok(userTokens);
    }
    [HttpPost]
    public IActionResult UpsertUserToken([FromBody] UserToken userToken)
    {
        if (userToken == null || string.IsNullOrEmpty(userToken.UserId))
        {
            return BadRequest("Invalid user token data.");
        }

        var existingToken = _context.UserTokens.FirstOrDefault(ut => ut.UserId == userToken.UserId);

        if (existingToken != null)
        {
            // Güncelleme işlemi
            existingToken.Token = userToken.Token;
            _context.UserTokens.Update(existingToken);
        }
        else
        {
            // Ekleme işlemi
            _context.UserTokens.Add(userToken);
        }

        _context.SaveChanges();
        return Ok(userToken);
    }
}

using envantyService.Models;
using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context, ILoginService loginService)
        {
            _context = context;
            _loginService = loginService;
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            // Kullanıcıyı doğrula
            var user = _loginService.Authenticate(request.Email, request.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Kullanıcı adı veya şifre yanlış" });
            }

            // Kullanıcıyı ve şirket bilgisini almak için join yapma
            var userWithCompany = (from u in _context.Logins
                                   join s in _context.Sirketlers on u.SirketlerId equals s.Id
                                   where u.Email == request.Email || u.UserId == request.Email
                                   select new 
                                   {
                                       FirstLogin = u.FirstLogin,
                                       UserId = u.UserId,
                                       Id = u.Id,
                                       Email = u.Email,
                                       Name = u.Name,
                                       Surname = u.Surname,
                                       Status = u.Status,
                                       Birthdate = u.Birthdate,
                                       Gender = u.Gender,
                                       EmploymentStart = u.EmploymentStart,
                                       EmploymentEnd = u.EmploymentEnd,
                                       DepartmanId = u.DepartmanId,
                                       DepartmanCategoryId = u.DepartmanCategoryId,
                                       UnvanlarId = u.ÜnvanlarId,
                                       SirketlerId = u.SirketlerId,
                                       CreateDate = u.CreateDate,
                                       RoleId = u.RoleId,
                                       CompanyName = s.Description,
                                       PhoneNumber=u.PhoneNumber
                                       // Şirket adı
                                   }).FirstOrDefault();

            // DTO oluşturma ve geri döndürme işlemleri
            var userDto = new UserDto
            {
                Id = userWithCompany.Id,
                UserId = userWithCompany.UserId,
                Email = userWithCompany.Email,
                Name = userWithCompany.Name,
                Surname = userWithCompany.Surname,
                Status = userWithCompany.Status,
                Birthdate = userWithCompany.Birthdate,
                Gender = userWithCompany.Gender,
                EmploymentStart = userWithCompany.EmploymentStart,
                EmploymentEnd = userWithCompany.EmploymentEnd,
                DepartmanId = userWithCompany.DepartmanId,
                DepartmanCategoryId = userWithCompany.DepartmanCategoryId,
                ÜnvanlarId = userWithCompany.UnvanlarId,
                SirketlerId = userWithCompany.SirketlerId,
                CreateDate = userWithCompany.CreateDate,
                RoleId = userWithCompany.RoleId,
                FirstLogin = userWithCompany.FirstLogin,
                CompanyName = userWithCompany.CompanyName, // Şirket adını DTO'ya ekle
                PhoneNumber=userWithCompany.PhoneNumber 
            };
            if (userDto.Status)
            {
                return Ok(userDto);
            }
            else
            {
                return BadRequest("Üzgünüz, ancak girdiğiniz bilgilere ait bir hesap bulunamadı. Lütfen bilgilerinizi kontrol edip tekrar deneyin veya destek ekibimizle iletişime geçin");
            }

           
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

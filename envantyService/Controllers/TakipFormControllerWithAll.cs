using envantyService.Models;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class TakipFormControllerWithAll : ControllerBase // Controller yerine ControllerBase
    {
        private readonly AppDbContext _context;

        public TakipFormControllerWithAll(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET HTTP yöntemi olarak tanımlandı
        public IActionResult Index()
        {
            var result = from tf in _context.TakipForms
                         join dc in _context.DepartmanCategories
                             on tf.DepartmanCategoryId equals dc.Id into departmanCategoryGroup
                         from dc in departmanCategoryGroup.DefaultIfEmpty()
                         join dp in _context.Departmans
                             on dc.DepartmanId equals dp.Id into departmanGroup
                         from dp in departmanGroup.DefaultIfEmpty()
                         where tf.Status != "Hayır" // Status "Hayır" haricindeki kayıtları al
                         select new
                         {
                             CategoryName = dc != null ? dc.Name : null,
                             Mail = dc != null ? dc.Mail : null,
                             Description = dp != null ? dp.Description : null,
                             TakipForm = tf
                         };

            // Toplam kayıt sayısını hesapla
            var totalRecords = result.Count();

            return Ok(new
            {
                TotalRecords = totalRecords,
                Data = result.ToList()
            }); // JSON olarak döndür
        }


    }

}

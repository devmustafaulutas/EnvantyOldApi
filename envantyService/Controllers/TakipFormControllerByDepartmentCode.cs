using envantyService.Models;
using Microsoft.AspNetCore.Mvc;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakipFormControllerByDepartmentCode : ControllerBase
    {
        private readonly AppDbContext _context;

        public TakipFormControllerByDepartmentCode(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{departmanKodu}")] // Departman kodunu parametre olarak alır
        public IActionResult GetByDepartmanKodu(int departmanKodu)
        {
           

                     var result = from tf in _context.TakipForms
                         join dp in _context.Departmans
                             on tf.DepartmanId equals dp.Id into departmanGroup
                         from dp in departmanGroup.DefaultIfEmpty()
                         where (tf.DepartmanId == departmanKodu && tf.DepartmanId != 0) // Departman koduna eşleşen
                               && ((tf.Status == "Açık" && tf.SorumluMail==null))|| (tf.DepartmanId == departmanKodu && tf.DepartmanCategoryId==0 && tf.Status != "Hayır")// Status "Hayır" haricindeki
                         select new
                         {
                             DepartmentName = dp != null ? dp.Description : null, // Departman adı
                             Description = dp != null ? dp.Description : null, // Departman açıklaması
                             TakipForm = tf // Takip formu nesnesi
                         };

            // Toplam kayıt sayısını hesapla
            var totalRecords = result.Count();

            return Ok(new
            {
                TotalRecords = totalRecords,
                Data = result.ToList()
            });
        }

        [HttpGet] // Departman kodunu parametre olarak alır
        public IActionResult GetExceptDepartmanKodu(int departman)
        {
            var result = from tf in _context.TakipForms
                         join dc in _context.DepartmanCategories on tf.DepartmanCategoryId equals dc.Id
                         join dp in _context.Departmans on dc.DepartmanId equals dp.Id
                         where  (tf.Status != "Hayır" && tf.Status != "Onaylandı" && tf.Status != "Reddedildi") // Status "Hayır" haricindeki kayıtları al 
                               && dp.Id != departman // Department ID'si 5 hariç
                         orderby tf.CreateDate descending // CreateDate'e göre azalan sıralama
                         select new
                         {
                             CategoryName = dc.Name,
                             dc.Mail,
                             dp.Description,
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

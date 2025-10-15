using envantyService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace envantyService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakipFormControllerAssignToMe : Controller
    {
        private readonly AppDbContext _context;

        public TakipFormControllerAssignToMe(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet] // GET HTTP yöntemi olarak tanımlandı
        public IActionResult Index([FromQuery] string email)
        {
            var result = from tf in _context.TakipForms
                         join dc in _context.DepartmanCategories on tf.DepartmanCategoryId equals dc.Id
                         join dp in _context.Departmans on dc.DepartmanId equals dp.Id
                         where tf.SorumluMail == email && (tf.Status != "Hayır" && tf.Status != "Onaylandı" && tf.Status != "Reddedildi") // Status "Hayır" haricindeki kayıtları al // E-posta adresine göre filtrele
                         orderby tf.CreateDate descending // CreateDate'e göre azalan sıralama
                         select new
                         {
                             CategoryName = dc.Name,
                             dc.Mail,
                             dp.Description,
                             TakipForm = new
                             {
                                 tf.Id,
                                 tf.TakipNumarısı,
                                 tf.NameSurname,
                                 Email = tf.ShowMail == true ? null : tf.Email, // ShowMail true ise Email alanını gizle
                                 tf.Title,
                                 tf.İçerik, // JsonPropertyName özelliğiyle uyumlu hale getirmek için
                                 tf.CreateDate,
                                 tf.Status,
                                 SorumluMail = tf.ShowMail, // ShowMail true ise SorumluMail alanını gizle
                                 tf.DepartmanCategoryId,
                                 tf.KayitTipi,
                                 tf.Onem_Derecesi,
                                 tf.DocumentPath,
                                 tf.AyınProjeKonu,
                                 tf.DepartmanId,
                                 tf.ShowMail
                             }
                         };

            // Toplam kayıt sayısını hesapla
            var totalRecords = result.Count();

            return Ok(new
            {
                TotalRecords = totalRecords,
                Data = result.ToList()
            }); // JSON olarak döndür
            //return Ok(result.ToList()); // JSON olarak döndür
        }

    }
}

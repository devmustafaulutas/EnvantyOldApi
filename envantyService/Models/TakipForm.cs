using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace envantyService.Models
{
    public class TakipForm
    {
        public int Id { get; set; }
        [JsonPropertyName("TakipNumarasi")]
        public int? TakipNumarısı { get; set; }
        public string? NameSurname { get; set; }
        public string? Email { get; set; }
        public string? Title { get; set; }
        [JsonPropertyName("ıcerik")]
        public string? İçerik { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public string?   SorumluMail { get; set; }
        public int ? DepartmanCategoryId { get; set; }
        public DepartmanCategory DepartmanCategory { get; set; }
        public string?KayitTipi { get; set; }
        public string?Onem_Derecesi { get; set; }
        public string? DocumentPath { get; set; }
        public string? AyınProjeKonu { get; set; }

        public int? DepartmanId { get; set; }

        public bool? ShowMail { get; set; }
  
    }

}

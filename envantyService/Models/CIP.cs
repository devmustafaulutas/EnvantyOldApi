using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace envantyService.Models
{
    public class CIP
    {
        public int Id { get; set; }

        public string? NameSurname { get; set; }
        public string? Eposta { get; set; }
        public int? DepartmanId { get; set; }
        public int? DepartmanCategoryId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? DocumentPath { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Status { get; set; }

        [JsonPropertyName("TakipNumarasi")]
        [Required]
        [Range(100000000, 999999999)]
        public int? TakipNumarısı { get; set; }
    }
}

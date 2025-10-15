using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace envantyService.Models
{
    public class AnketYonetimi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? SurveyNo { get; set; }

        [Required]
        public string? SurveyMetaData { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Status { get; set; }
        public string? formTitle { get; set; }
        public int EtkinlikId { get; set; }
        public DateTime LastJoinDate { get; set; }
    }
}
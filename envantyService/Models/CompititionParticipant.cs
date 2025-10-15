using System.ComponentModel.DataAnnotations;

namespace envantyService.Models
{
    public class CompititionParticipant
    {
        [Key]
        public int Id { get; set; } // Optional, use as a primary key if needed
        [Required]
        [StringLength(50)]
        public string UserMail { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public int yarışmaYönetimiId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace envantyService.Models
{
    public class Etkinlik
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int SirketlerId { get; set; }
        public DateTime EtkinlikTarihi { get; set; }
        public DateTime EtkinlikSaati { get; set; }
        public bool MustJoinEvent { get; set; }
        public bool Status { get; set; }
        [MaxLength]
        public string? ImagePath { get; set; }
        public string? FilePath { get; set; }

    }

}

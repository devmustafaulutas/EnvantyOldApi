namespace envantyService.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class AdminCevaplama
    {
        [Key]
        public int Id { get; set; }

        [Column("Cevapİçeriği")]
        public string? CevapIcerigi { get; set; }

        public DateTime? CevapTarihi { get; set; }

        // Foreign key: Hangi takip formuna ait olduğu
        public int TakipFormId { get; set; }
        public TakipForm TakipForm { get; set; }
        public string? AdminName { get; set; }
       
    }

}

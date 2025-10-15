namespace envantyService.Models
{
    public class Omus
    {
        public int Id { get; set; }
        public string ?NameSurname { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; } // Nullable
        public string? DocumentPath { get; set; } // Nullable
        public int TakipNumarisi { get; set; }
        public int omu_DepartmanId { get; set; }
        public int IyileştirmetipiId { get; set; }
        public string? Status { get; set; } // Nullable
        public int? DepartmanId { get; set; } // Nullable
        public string? Iyilestirme_Degerlendirmesi { get; set; } // Nullable
        public DateTime CreateDate { get; set; }
        public string? SorumluKisi { get; set; } // Nullable
        public string? KayitTipi { get; set; } // Nullable
        
    }

}

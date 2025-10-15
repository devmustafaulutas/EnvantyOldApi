namespace envantyefe.Entityframeworks
{
    public class Doctor
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public string? DoctorShift { get; set; }
        public string? NurseShift { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool Status { get; set; }
    }
}

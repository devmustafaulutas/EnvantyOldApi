namespace envantyService
{
    public class UserDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Status { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public DateTime EmploymentStart { get; set; }
        public DateTime EmploymentEnd { get; set; }
        public int DepartmanId { get; set; }
        public int DepartmanCategoryId { get; set; }
        public int ÜnvanlarId { get; set; }
        public int SirketlerId { get; set; }
        public DateTime CreateDate { get; set; }
        public int RoleId { get; set; }
        public string CompanyName { get; set; }
        public bool FirstLogin { get; set; }
        public string? PhoneNumber { get; set; }
    }

}

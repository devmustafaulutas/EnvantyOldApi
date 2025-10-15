namespace envantyService.Models
{
    public class Departman
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<DepartmanCategory> DepartmanCategories { get; set; }
    }

}

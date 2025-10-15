namespace envantyService.Models
{
    public class DepartmanCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TakipForm> TakipForms { get; set; }

        
        public string Mail { get; set; }
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }
    }


}

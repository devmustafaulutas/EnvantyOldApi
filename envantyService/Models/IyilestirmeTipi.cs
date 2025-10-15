using System.ComponentModel.DataAnnotations;

namespace envantyService.Models
{
    public class IyilestirmeTipi
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

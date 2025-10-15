using System.Text.Json.Serialization;

namespace envantyService.Models
{
    public class CompetitionAdministration
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int SirketlerId { get; set; }
        [JsonPropertyName("YarismaTarihi")]
        public DateTime YarışmaTarihi { get; set; }
        [JsonPropertyName("YarismaSaati")]
        public DateTime YarışmaSaati { get; set; }
        public bool MustJoinEvent { get; set; }
        public bool Status { get; set; }
    }

}

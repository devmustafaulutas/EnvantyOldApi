using Microsoft.AspNetCore.Mvc;

namespace envantyService.Models
{
    public class Ceokösesis
    {
       
            /// <summary>
            /// The unique identifier for the record.
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// The title of the Ceokösesis entry.
            /// </summary>
            public string Title { get; set; }

            /// <summary>
            /// The content of the Ceokösesis entry.
            /// </summary>
            public string Content { get; set; }

            /// <summary>
            /// The path to the associated image.
            /// </summary>
            public string? ImagePath { get; set; }
            public string? FilePath { get; set; }
            /// <summary>
            /// The date and time when the entry was shared.
            /// </summary>
            public DateTime ShareDate { get; set; }

            /// <summary>
            /// The status indicating if the entry is active or not.
            /// </summary>
            public bool? Status { get; set; }
        

    }

}

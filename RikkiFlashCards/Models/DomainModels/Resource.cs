using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AnkiFlashCards.Models.Domain
{
    public class Resource : IEntityModel
    {
        [DisplayName("ID")]
        public int ResourceId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [Required]
        public String Title { get; set; }
        public String Url { get; set; }
        public ResourceType Type { get; set; }
        public String Notes { get; set; }
        public int DeckCount { 
            get
            {
                return (Decks!=null)?(Decks.Count()):0;
            }
        }
        public ICollection<Deck> Decks { get; set; }

        public bool IsDeleted { get; set; }
    }

    public enum ResourceType
    {
        Book=0,
        WebSite=1,
        Video=2
    }
}

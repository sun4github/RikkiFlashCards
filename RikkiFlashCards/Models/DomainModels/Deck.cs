using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public class Deck : IEntityModel
    {
        [DisplayName("Id")]
        public int DeckId { get; set; }
        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public Resource Resource { get; set; }
        [DisplayName("Title")]
        [Required]
        public String Title { get; set; }
        [DisplayName("Shared")]
        public bool IsShared { get; set; }
        [DisplayName("Number of Revisions")]
        public int RevisionCount { 
            get {
                return Revisions.Count();
            }
        }
        public ICollection<Revision> Revisions { get; set; }
        [DisplayName("Number of Cards")]
        public int CardCount
        {
            get
            {
                
                return (Cards!=null)?Cards.Count():0;
            }
        }
        public ICollection<Card> Cards { get; set; }

        public bool IsDeleted { get; set; }
    }
}

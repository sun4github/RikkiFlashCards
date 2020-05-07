using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class CardListDto
    {
        public int DeckId { get; set; }
        public int ResourceId { get; set; }
        [DisplayName("Resource")]
        public string ResourceTitle { get; set; }
        [DisplayName("Deck")]
        public String DeckTitle { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        [DisplayName("Shared")]
        public bool IsShared { get; set; }
        [DisplayName("Number of Cards")]
        public int CardCount { get; set; }
        public IEnumerable<Card> Cards { get; set; }
        [Required]
        [DisplayName("Search For")]
        public string SearchFor { get; set; }
        public int TotalCardCount { get; set; }
    }
}

using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class DeckListDto
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int ResourceId { get; set; }
        public int SubjectId { get; set; }
        public String ResourceTitle { get; set; }
        public IEnumerable<DeckViewDto> Decks { get; set; }
        [DisplayName("Last Revised Date")]
        public string LastRevisedDate { get; set; }
        public int TotalDeckCount { get; set; }
    }
}

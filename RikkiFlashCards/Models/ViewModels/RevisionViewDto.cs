using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class RevisionViewDto
    {
        [DisplayName("Id")]
        public int RevisionId { get; set; }
        public int ResourceId { get; set; }
        public int DeckId { get; set; }
        [DisplayName("Deck")]
        public String DeckTitle { get; set; }
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }
        public int PercentCorrect { get; set; }
        [DisplayName("Revised Count")]
        public int RevisedCount { get; set; }
        [DisplayName("Is this a exam ?")]
        public bool IsExam { get; set; }
    }
}

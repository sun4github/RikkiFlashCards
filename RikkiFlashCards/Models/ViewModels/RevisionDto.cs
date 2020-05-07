using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class RevisionDto
    {
        public int RevisionId { get; set; }
        public int DeckId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PercentCorrect { get; set; }
        public int RevisedCount { get; set; }
    }
}

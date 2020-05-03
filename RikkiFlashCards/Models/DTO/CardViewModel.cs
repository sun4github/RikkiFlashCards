using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnkiFlashCards.Models.Domain;

namespace AnkiFlashCards.Models.DTO
{
    public class CardViewModel
    {
        public int CardId { get; set; }
        public int DeckId { get; set; }
        public String Front { get; set; }
        public String Back { get; set; }
        public DifficultyLevel Level { get; set; }
    }
}

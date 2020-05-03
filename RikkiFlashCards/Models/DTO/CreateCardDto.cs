using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AnkiFlashCards.Models.DTO
{
    public class CreateCardDto
    {
        [DisplayName("ID")]
        public int CardId { get; set; }
        public int DeckId { get; set; }
        [DisplayName("Deck")]
        public String DeckTitle { get; set; }
        [DisplayName("Question")]
        [Required]
        [StringLength(5000,MinimumLength = 5)]
        public String Front { get; set; }
        [DisplayName("Answer")]
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public String Back { get; set; }
        public DifficultyLevel Level { get; set; }
    }
}

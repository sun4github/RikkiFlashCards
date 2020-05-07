using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class CardViewDto
    {
      

        [DisplayName("ID")]
        public int CardId { get; set; }
        public int DeckId { get; set; }
        public int RevisionId { get; set; }
        public bool IsExam { get; set; }
        [DisplayName("Deck")]
        public string DeckTitle { get; set; }
        [DisplayName("Question")]
        public string Front
        {
            get;set;
        }
        [DisplayName("Answer")]
        public string Back
        {
            get;set;
        }
        public DifficultyLevel Level { get; set; }
       
    }

}

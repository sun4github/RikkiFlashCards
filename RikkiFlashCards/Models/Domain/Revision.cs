using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public class Revision : IEntityModel
    {
        [DisplayName("Id")]
        public int RevisionId { get; set; }
        [ForeignKey("Deck")]
        public int DeckId { get; set; }
        public Deck Deck { get; set; }
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }
        public int PercentCorrect { get; set; }
        public int RevisedCount { get; set; }
        public int TotalCardCount { get; set; }
        public bool IsExam { get; set; }
        public List<int> CardOrderList { get; set; }
        public bool IsDeleted { get; set; }
        [DefaultValue(false)]
        public bool IsProperlyClosed { get; set; }
        public int LastCardId { get; set; }
    }
}

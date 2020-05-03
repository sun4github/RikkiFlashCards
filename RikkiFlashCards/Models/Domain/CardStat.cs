using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.Domain
{
    public class CardStat
    {
        [ForeignKey("Card")]
        public int CardId { get; set; }
        public Card Card { get; set; }
        [ForeignKey("Revision")]
        public int RevisionId { get; set; }
        public Revision Revision { get; set; }
        public DateTime ExaminedTime { get; set; }
        public bool IsResponseCorrect { get; set; }
    }
}

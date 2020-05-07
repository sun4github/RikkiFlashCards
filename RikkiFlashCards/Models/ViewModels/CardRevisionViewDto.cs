using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Models.DTO
{
    public class CardRevisionViewDto : CardViewDto
    {
        public CardRevisionViewDto(CardViewDto cardViewDto)
        {
            this.CardId = cardViewDto.CardId;
            this.DeckId = cardViewDto.DeckId;
            this.RevisionId = cardViewDto.RevisionId;
            this.IsExam = cardViewDto.IsExam;
            this.DeckTitle = cardViewDto.DeckTitle;
            this.Front = cardViewDto.Front;
            this.Back = cardViewDto.Back;

            TotalRevisedCount = 0;
            TotalCardCount = 0;
        }
        public int TotalRevisedCount { get; set; }
        public int TotalCardCount { get; set; }
    }
}

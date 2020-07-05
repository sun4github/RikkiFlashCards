using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Services.Contracts
{
    public interface ICodeRenderService
    {
        public Card EncodeCardContentForReadonlyView(Card card);

        public Card RestoreNewLine(Card card);
    }
}

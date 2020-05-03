using AnkiFlashCards.Data.Repositories.Contracts;
using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Data.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(RikkiFlashCardsDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

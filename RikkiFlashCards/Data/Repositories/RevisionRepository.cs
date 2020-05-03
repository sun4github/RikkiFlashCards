using AnkiFlashCards.Data.Repositories.Contracts;
using AnkiFlashCards.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Data.Repositories
{
    public class RevisionRepository : RepositoryBase<Revision>, IRevisionRepository
    {
        public RevisionRepository(RikkiFlashCardsDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}

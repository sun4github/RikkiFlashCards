using AnkiFlashCards.Data.Repositories;
using AnkiFlashCards.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RikkiFlashCardsDbContext applicationDbContext;
        private SubjectRepository subjectRepository;
        private ResourceRepository resourceRepository;
        private DeckRepository deckRepository;
        private CardRepository cardRepository;
        private RevisionRepository revisionRepository;

        public RepositoryWrapper(RikkiFlashCardsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public ISubjectRepository Subject
        {
            get
            {
                if (subjectRepository == null)
                    subjectRepository = new SubjectRepository(this.applicationDbContext);

                return subjectRepository;
            }
        }

        public IResourceRepository Resource { get
            {
                if (resourceRepository == null)
                    resourceRepository = new ResourceRepository(this.applicationDbContext);

                return resourceRepository;
            }
        }

        public IDeckRepository Deck { 
            get
            {
                if (deckRepository == null)
                    deckRepository = new DeckRepository(this.applicationDbContext);

                return deckRepository;
            }
        }

        public ICardRepository Card { 
            get
            {
                if (cardRepository == null)
                    cardRepository = new CardRepository(this.applicationDbContext);

                return cardRepository;
            }
        }

        public IRevisionRepository Revision { 
            get
            {
                if (revisionRepository == null)
                    revisionRepository = new RevisionRepository(this.applicationDbContext);

                return revisionRepository;
            }
        }

        public void Save()
        {
            applicationDbContext.SaveChanges();
        }
    }
}

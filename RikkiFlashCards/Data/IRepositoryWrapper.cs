using AnkiFlashCards.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnkiFlashCards.Data
{
    public interface IRepositoryWrapper
    {
        public ISubjectRepository Subject { get; }
        public IResourceRepository Resource { get;  }
        public IDeckRepository Deck { get;  }
        public ICardRepository Card { get;  }
        public IRevisionRepository Revision { get;  }
        public void Save();
    }
}

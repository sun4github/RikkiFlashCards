using AnkiFlashCards.Data;
using AnkiFlashCards.Data.Repositories;
using RikkiFlashCards.Data.Repositories.Contracts;
using RikkiFlashCards.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RikkiFlashCards.Data.Repositories
{
    public class ImageFileRepository : RepositoryBase<ImageFile>, IImageFileRepository
    {
        public ImageFileRepository(RikkiFlashCardsDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}

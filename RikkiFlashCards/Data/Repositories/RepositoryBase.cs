using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AnkiFlashCards.Data.Repositories.Contracts;
using AnkiFlashCards.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnkiFlashCards.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntityModel
    {
        private readonly RikkiFlashCardsDbContext applicationDbContext;

        public RepositoryBase(RikkiFlashCardsDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public void Create(T entity)
        {
            this.applicationDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this.applicationDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.applicationDbContext.Set<T>().AsTracking();
                //.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            var baseSet = this.applicationDbContext.Set<T>();
            return baseSet.Where(expression);
                //.AsNoTracking();
        }

        public void Update(T entity)
        {
            this.applicationDbContext.Update(entity);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DontGetLost.Contracts;

namespace DontGetLost.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public Repository(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IQueryable<TEntity> FindAll()
        {
            return this.RepositoryContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return this.RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public void Create(TEntity entity)
        {
            this.RepositoryContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            this.RepositoryContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this.RepositoryContext.Set<TEntity>().Remove(entity);
        }
    }
}

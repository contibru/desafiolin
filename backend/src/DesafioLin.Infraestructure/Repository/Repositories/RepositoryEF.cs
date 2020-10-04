using DesafioLin.Domain.Interfaces;
using DesafioLin.Infraestructure.Context;
using DesafioLin.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesafioLin.Infraestructure.Repository
{
    /*
     * Essa classe Abstrata irá ser a base para todos os repositórios usando EntityFramework.
     */

    public class RepositoryEF<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {
        protected readonly DesafioLinContext dbContext;

        public RepositoryEF(DesafioLinContext context)
        {
            dbContext = context;
        }

        public virtual void Insert(TEntity obj)
        {
            dbContext.Add(obj);
        }

        public virtual IEnumerable<TEntity> SQL(string sql)
        {
            return dbContext.Set<TEntity>().FromSqlRaw(sql).ToList();
        }

        public virtual void ExecuteQuery(string sql)
        {
            dbContext.Database.ExecuteSqlRaw(sql);
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public virtual TEntity GetById(int? id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity obj)
        {
            dbContext.Set<TEntity>().Remove(obj);
        }

        public void Update(List<TEntity> obj)
        {
            dbContext.UpdateRange(obj);
        }

        public void Update(TEntity obj)
        {
            dbContext.Set<TEntity>().Update(obj);
        }

        public void Dispose()
        {
        }

        public virtual void Commit()
        {
            dbContext.SaveChanges();
        }
    }
}
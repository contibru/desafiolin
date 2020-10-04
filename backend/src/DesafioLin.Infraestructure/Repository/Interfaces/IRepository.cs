using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesafioLin.Infraestructure.Repository.Interfaces
{
    /*
     * Essa interface é a base para a Classe Repository que será usada por todos os Repositórios.
     *
     */

    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Insert(TEntity obj);

        IEnumerable<TEntity> SQL(string sql);

        void ExecuteQuery(string sql);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int? id);

        void Delete(TEntity obj);

        void Update(TEntity obj);

        void Update(List<TEntity> obj);

        void Commit();
    }
}
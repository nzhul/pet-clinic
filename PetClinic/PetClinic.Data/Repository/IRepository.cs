using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity instance);

        void Remove(TEntity instance);

        TEntity One(int id);

        TEntity FindOne(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> All();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        bool Exists(Expression<Func<TEntity, bool>> predicate);

        void UpdateSchema();
    }
}

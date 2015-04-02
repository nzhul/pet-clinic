using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace PetClinic.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: class, IEntity
    {
        protected OpenAccessContext Context { get; private set; }

        public Repository(IContextFactory contextFactory)
            :this(contextFactory.Get())
        {
        }

        protected Repository(OpenAccessContext context)
        {
            Context = context;
        }

        public virtual void Add(TEntity instance)
        {
            Context.Add(instance);
        }

        public virtual void Remove(TEntity instance)
        {
            Context.Delete(instance);
        }

        public virtual TEntity One(int id)
        {
            return Context.GetAll<TEntity>().SingleOrDefault(e => e.Id == id);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.GetAll<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return Context.GetAll<TEntity>();
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.GetAll<TEntity>().Where(predicate);
        }

        public virtual int Count()
        {
            return Context.GetAll<TEntity>().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.GetAll<TEntity>().Count(predicate);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.GetAll<TEntity>().Any(predicate);
        }

        public virtual void UpdateSchema()
        {
            var handler = Context.GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                    throw;
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }
    }
}

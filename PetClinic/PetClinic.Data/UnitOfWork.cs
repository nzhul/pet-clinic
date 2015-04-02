using PetClinic.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Telerik.OpenAccess.OpenAccessContext context;

        public UnitOfWork(IContextFactory contextFactory)
            :this(contextFactory.Get())
        {
        }

        protected UnitOfWork(Telerik.OpenAccess.OpenAccessContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            context.SaveChanges();
        }
    }
}

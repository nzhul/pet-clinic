using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace PetClinic.Data
{
    public class ContextFactory : IContextFactory
    {
        private bool isDisposed;

        private readonly string connectionString;

        private OpenAccessContext context;

        public ContextFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public OpenAccessContext Get()
        {
            if (context == null)
            {
                var metadata = new DataMetadataSource().GetModel();
                context = new OpenAccessContext(connectionString, new BackendConfiguration() { Backend = "mssql" }, metadata);
            }

            return context;
        }

        ~ContextFactory()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed && disposing && (context != null))
            {
                context.Dispose();
            }

            isDisposed = true;
        }
    }
}

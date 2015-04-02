using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace PetClinic.Data
{
    public interface IContextFactory : IDisposable
    {
        OpenAccessContext Get();
    }
}

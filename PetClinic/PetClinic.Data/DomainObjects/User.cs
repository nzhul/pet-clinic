using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class User : IEntity, INamable
    {

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public virtual bool IsLockedOut { get; set; }
    }
}

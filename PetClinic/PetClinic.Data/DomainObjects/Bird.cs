using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Bird : Pet
    {
        public virtual int PartnerId { get; set; }

        public virtual Bird Partner { get; set; }
    }
}

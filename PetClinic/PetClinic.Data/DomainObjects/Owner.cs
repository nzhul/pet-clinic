using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Owner : IEntity, INamable
    {
        private IList<Pet> pets = new List<Pet>();

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual IList<Pet> Pets
        {
            get
            {
                return this.pets;
            }

            set
            {
                this.pets = value;
            }
        }
    }
}

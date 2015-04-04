using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.ViewModels
{
    public class OwnerDetailsViewModel
    {
        private IList<Pet> pets = new List<Pet>();

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Pet> Pets
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

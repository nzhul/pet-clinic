using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Dog : Pet
    {
        public virtual string FavouriteFood { get; set; }

        public virtual string FavouriteGame { get; set; }

        public virtual bool IsAggressiveTowardsOtherPeople { get; set; }
    }
}

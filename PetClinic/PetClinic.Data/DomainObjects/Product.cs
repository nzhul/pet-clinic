using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetClinic.Data
{
    public class Product : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int Price { get; set; }

        public virtual string Description { get; set; }

        public virtual Category Category { get; set; }

        public virtual int CategoryId { get; set; }
    }
}

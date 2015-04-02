using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetClinic.Data
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}

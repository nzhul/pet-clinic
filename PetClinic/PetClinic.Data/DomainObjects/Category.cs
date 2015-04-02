using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Category : IEntity
    {
        public Category()
        {
            this.Products = new List<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }

    }
}

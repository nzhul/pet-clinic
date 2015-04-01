using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Discontinued { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

    }
}

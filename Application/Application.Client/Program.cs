using Application.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DataContext dbContext = new DataContext())
            {
                dbContext.UpdateSchema();

                Product p = new Product
                {
                    Name = "MyProduct"
                };

                Category c = new Category
                {
                    Name = "MyCategory"
                };

                p.Category = c;

                dbContext.Add(p);
                dbContext.Add(c);

                dbContext.SaveChanges();

                foreach (Product item in dbContext.Products)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
    }
}

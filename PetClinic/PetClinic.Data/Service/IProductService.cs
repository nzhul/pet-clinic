using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Service
{
    public interface IProductService
    {
        CategoryViewModel GetCategoryById(int id);

        IEnumerable<CategoryViewModel> GetCategories();

        ProductViewModel GetProductById(int id);

        IEnumerable<ProductViewModel> GetProducts();

        int CreateCategory(string name);
        int CreateProduct(string name, int categoryId);
    }
}

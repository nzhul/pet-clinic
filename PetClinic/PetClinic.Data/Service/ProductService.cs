using PetClinic.Data.DomainObjects;
using PetClinic.Data.Infrastructure;
using PetClinic.Data.Repository;
using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IRepository<Category> categoryRepository, IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public virtual CategoryViewModel GetCategoryById(int id)
        {
            Category category = categoryRepository.One(id);
            return CreateCategoryViewModel(category);
        }

        public virtual IEnumerable<CategoryViewModel> GetCategories()
        {
            IEnumerable<CategoryViewModel> category = categoryRepository.All().Select(CreateCategoryViewModel);
            return category;
        }

        public virtual ProductViewModel GetProductById(int id)
        {
            Product product = productRepository.One(id);
            return CreateProductViewModel(product);
        }

        public virtual IEnumerable<ProductViewModel> GetProducts()
        {
            IEnumerable<ProductViewModel> product = productRepository.All().Select(CreateProductViewModel);
            return product;
        }


        public virtual int CreateCategory(string name)
        {
            Category newCategory = new Category
            {
                Name = name
            };

            categoryRepository.Add(newCategory);

            unitOfWork.Commit();

            return newCategory.Id;

        }

        public virtual int CreateProduct(string name, int categoryId)
        {
            throw new NotImplementedException();
        }

        private CategoryViewModel CreateCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
            };
        }

        private ProductViewModel CreateProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}

using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public virtual UserViewModel GetById(int id)
        {
            User user = repository.One(id);
            return CreateUserViewModel(user);
        }

        public virtual UserViewModel GetByName(string name)
        {
            string lowerCaseName = name.ToLower(CultureInfo.CurrentCulture);

            User user = repository.FindOne(u => u.Name == lowerCaseName);

            return CreateUserViewModel(user);
        }

        private static UserViewModel CreateUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                IsLockedOut = user.IsLockedOut
            };
        }
    }
}

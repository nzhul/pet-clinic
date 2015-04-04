using PetClinic.Data.DomainObjects;
using PetClinic.Data.Infrastructure;
using PetClinic.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PetClinic.Web.Account
{
    public class MembershipService : IMembershipService
    {
        private readonly IRepository<User> repository;
        private readonly IUnitOfWork unitOfWork;

        public MembershipService(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public int MinPasswordLength
        {
            get { return 4; }
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = repository.FindOne(u => u.Name == userName);

            return (user != null) && !user.IsLockedOut && (user.Password.Equals(password, StringComparison.Ordinal));
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (repository.Exists(u => u.Name == userName))
            {
                return MembershipCreateStatus.DuplicateUserName;
            }

            if (repository.Exists(u => u.Email == email))
            {
                return MembershipCreateStatus.InvalidEmail;
            }

            User user = new User
            {
                Name = userName,
                Password = password,
                Email = email,
                IsLockedOut = false
            };

            repository.Add(user);
            unitOfWork.Commit();

            return MembershipCreateStatus.Success;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            User user = repository.FindOne(u => u.Name == userName);

            if ((user != null) && (!user.IsLockedOut) && (user.Password.Equals(oldPassword, StringComparison.Ordinal)))
            {
                user.Password = newPassword;
                unitOfWork.Commit();
            }

            return false;
        }
    }
}
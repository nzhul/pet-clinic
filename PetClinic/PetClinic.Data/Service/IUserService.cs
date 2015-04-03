using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Service
{
    public interface IUserService
    {
        UserViewModel GetById(int id);

        UserViewModel GetByName(string name);
    }
}

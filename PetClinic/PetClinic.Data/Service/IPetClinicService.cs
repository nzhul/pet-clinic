using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.Service
{
    public interface IPetClinicService
    {

        OwnerViewModel GetOwnerById(int id);

        IEnumerable<OwnerViewModel> GetOwners();

        PetViewModel GetPetById(int id);

        IEnumerable<PetViewModel> GetPets();

        int CreateOwner(string name);
        int CreatePet(string name, int ownerId);
    }
}

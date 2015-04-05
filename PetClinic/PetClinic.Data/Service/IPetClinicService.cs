using PetClinic.Data.DomainObjects.Enums;
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

        OwnerDetailsViewModel GetOwnerDetailsById(int id);

        IEnumerable<OwnerViewModel> GetOwners();

        PetViewModel GetPetById(int id);

        IEnumerable<PetViewModel> GetPets();

        int CreateOwner(string name);

        int EditOwner(int id, string name);

        int CreatePet(string name, int ownerId);

        int CreateCat(string name, int ownerId, string breed, int age, int genderType, int numberOfHoursSpentSleeping, string favouriteFood);
        int CreateDog(string name, int ownerId, string breed, int age, int genderType, string favouriteFood, string favouriteGame, bool isAggressive);
        int CreateBird(string name, int ownerId, string breed, int age, int genderType, int? partnerId);

    }
}

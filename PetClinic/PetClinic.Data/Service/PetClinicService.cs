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
    public class PetClinicService : IPetClinicService
    {
        private readonly IRepository<Owner> ownerRepository;
        private readonly IRepository<Pet> petRepository;
        private readonly IUnitOfWork unitOfWork;

        public PetClinicService(IRepository<Owner> ownerRepository, IRepository<Pet> petRepository, IUnitOfWork unitOfWork)
        {
            this.ownerRepository = ownerRepository;
            this.petRepository = petRepository;
            this.unitOfWork = unitOfWork;
        }

        public virtual OwnerViewModel GetOwnerById(int id)
        {
            Owner owner = ownerRepository.One(id);
            return CreateOwnerViewModel(owner);
        }

        public virtual IEnumerable<OwnerViewModel> GetOwners()
        {
            IEnumerable<OwnerViewModel> owner = ownerRepository.All().Select(CreateOwnerViewModel);
            return owner;
        }

        public virtual PetViewModel GetPetById(int id)
        {
            Pet pet = petRepository.One(id);
            return CreatePetViewModel(pet);
        }

        public virtual IEnumerable<PetViewModel> GetPets()
        {
            IEnumerable<PetViewModel> pet = petRepository.All().Select(CreatePetViewModel);
            return pet;
        }


        public virtual int CreateOwner(string name)
        {
            throw new NotImplementedException();
        }

        public virtual int CreatePet(string name, int ownerId)
        {
            throw new NotImplementedException();
        }

        private OwnerViewModel CreateOwnerViewModel(Owner owner)
        {
            return new OwnerViewModel
            {
                Id = owner.Id,
                Name = owner.Name,
                PetsCount = owner.Pets.Count()
            };
        }

        private PetViewModel CreatePetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Owner = pet.Owner,
                OwnerId = pet.OwnerId,
                Age = pet.Age,
                Breed = pet.Breed
            };
        }
    }
}

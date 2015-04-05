using PetClinic.Data.DomainObjects;
using PetClinic.Data.DomainObjects.Enums;
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
        private readonly IRepository<Bird> birdRepository;
        private readonly IUnitOfWork unitOfWork;

        public PetClinicService(IRepository<Owner> ownerRepository, IRepository<Pet> petRepository,IRepository<Bird> birdRepository, IUnitOfWork unitOfWork)
        {
            this.ownerRepository = ownerRepository;
            this.petRepository = petRepository;
            this.birdRepository = birdRepository;
            this.unitOfWork = unitOfWork;
        }

        public virtual OwnerViewModel GetOwnerById(int id)
        {
            Owner owner = ownerRepository.One(id);
            return CreateOwnerViewModel(owner);
        }

        public virtual OwnerDetailsViewModel GetOwnerDetailsById(int id)
        {
            Owner owner = ownerRepository.One(id);
            return CreateOwnerDetailsViewModel(owner);
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
            Owner owner = new Owner
            {
                Name = name
            };

            ownerRepository.Add(owner);
            unitOfWork.Commit();

            return owner.Id;
        }

        public virtual int EditOwner(int id, string name)
        {
            Owner theOwner = ownerRepository.One(id);
            theOwner.Name = name;
            unitOfWork.Commit();
            return theOwner.Id;
        }

        public virtual int CreatePet(string name, int ownerId)
        {
            Owner selectedOwner = ownerRepository.FindOne(x => x.Id == ownerId);

            Pet newPet = new Pet
            {
                Name = name,
                Owner = selectedOwner
            };

            petRepository.Add(newPet);
            unitOfWork.Commit();

            return newPet.Id;
        }

        public virtual int CreateCat(string name, int ownerId, string breed, int age, int genderType, int numberOfHoursSpentSleeping, string favouriteFood)
        {
            Owner selectedOwner = ownerRepository.FindOne(x => x.Id == ownerId);
            Gender gender = Gender.Male;
            switch (genderType)
            {
                case 1:
                    gender = Gender.Male;
                    break;
                default:
                case 2:
                    gender = Gender.Female;
                    break;
            }

            Pet newCat = new Cat
            {
                Name = name,
                Owner = selectedOwner,
                Breed = breed,
                Age = age,
                Gender = gender,
                NumberOfHoursSpentSleeping = numberOfHoursSpentSleeping,
                FavouriteFood = favouriteFood
            };

            petRepository.Add(newCat);
            unitOfWork.Commit();

            return newCat.Id;
        }


        public int CreateDog(string name, int ownerId, string breed, int age, int genderType, string favouriteFood, string favouriteGame, bool isAggressive)
        {
            Owner selectedOwner = ownerRepository.FindOne(x => x.Id == ownerId);
            Gender gender = Gender.Male;
            switch (genderType)
            {
                case 1:
                    gender = Gender.Male;
                    break;
                default:
                case 2:
                    gender = Gender.Female;
                    break;
            }

            Pet newDog = new Dog
            {
                Name = name,
                Owner = selectedOwner,
                Breed = breed,
                Age = age,
                Gender = gender,
                FavouriteGame = favouriteGame,
                FavouriteFood = favouriteFood,
                IsAggressiveTowardsOtherPeople = isAggressive
            };

            petRepository.Add(newDog);
            unitOfWork.Commit();

            return newDog.Id;
        }

        public int CreateBird(string name, int ownerId, string breed, int age, int genderType, int? partnerId)
        {
            Owner selectedOwner = ownerRepository.One(ownerId);
            Bird partner = birdRepository.One((int)partnerId);
            
            Gender gender = Gender.Male;
            switch (genderType)
            {
                case 1:
                    gender = Gender.Male;
                    break;
                default:
                case 2:
                    gender = Gender.Female;
                    break;
            }

            Pet newBird = new Bird
            {
                Name = name,
                Owner = selectedOwner,
                Breed = breed,
                Age = age,
                Gender = gender,
                Partner = partner
            };

            petRepository.Add(newBird);
            unitOfWork.Commit();

            return newBird.Id;
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

        private OwnerDetailsViewModel CreateOwnerDetailsViewModel(Owner owner)
        {
            return new OwnerDetailsViewModel
            {
                Id = owner.Id,
                Name = owner.Name,
                Pets = owner.Pets
            };
        }

        private PetViewModel CreatePetViewModel(Pet pet)
        {
            string petType = String.Empty;
            if (pet is Cat)
            {
                petType = "Cat";
            }
            else if (pet is Dog)
            {
                petType = "Dog";
            }
            else if (pet is Bird)
            {
                petType = "Bird";
            }
            else
            {
                petType = "Unknown";
            }
            return new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Owner = pet.Owner,
                OwnerId = pet.OwnerId,
                Age = pet.Age,
                Breed = pet.Breed,
                PetType = petType
            };
        }
    }
}

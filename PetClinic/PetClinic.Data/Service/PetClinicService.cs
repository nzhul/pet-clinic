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
        private readonly IRepository<Examination> examinationRepository;
        private readonly IUnitOfWork unitOfWork;

        public PetClinicService(IRepository<Owner> ownerRepository, 
                                IRepository<Pet> petRepository,
                                IRepository<Bird> birdRepository,
                                IRepository<Examination> examinationRepository,
                                IUnitOfWork unitOfWork)
        {
            this.ownerRepository = ownerRepository;
            this.petRepository = petRepository;
            this.birdRepository = birdRepository;
            this.examinationRepository = examinationRepository;
            this.unitOfWork = unitOfWork;
        }

        public virtual OwnerViewModel GetOwnerById(int id)
        {
            Owner owner = ownerRepository.One(id);
            return CreateOwnerViewModel(owner);
        }

        public virtual ExaminationViewModel GetExaminationById(int id)
        {
            Examination examination = examinationRepository.One(id);
            return CreateExaminationViewModel(examination);
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

        public IEnumerable<ExaminationViewModel> GetExaminations()
        {
            IEnumerable<ExaminationViewModel> examinations = examinationRepository.All().Select(CreateExaminationViewModel);
            return examinations;
        }

        public IEnumerable<ExaminationViewModel> GetExaminationsByDate(DateTime date)
        {
            IEnumerable<ExaminationViewModel> examinations =
                examinationRepository.All().Where(x => x.Date.Day == date.Day).Select(CreateExaminationViewModel);
            return examinations;
        }


        public virtual IEnumerable<PetViewModel> GetPetsFiltered(int ownerId, int typeId)
        {
            var petsQuerable = petRepository
                .All()
                .Where(x => x.OwnerId == ownerId).AsQueryable();

            switch (typeId)
            {
                case 1:
                    petsQuerable = petsQuerable.Where(x => x is Cat);
                    break;
                case 2:
                    petsQuerable = petsQuerable.Where(x => x is Dog);
                    break;
                case 3:
                    petsQuerable = petsQuerable.Where(x => x is Bird);
                    break;
                default:
                    break;
            }

            IEnumerable<PetViewModel> pets = petsQuerable.Select(CreatePetViewModel);

            return pets;
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
            Bird partner = new Bird();
            if (partnerId == null)
            {
                partner = null;
            }
            else
            {
                partner = birdRepository.One((int)partnerId);
            }
            
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

        private ExaminationViewModel CreateExaminationViewModel(Examination examination)
        {
            return new ExaminationViewModel
            {
                Id = examination.Id,
                Date = examination.Date,
                ExaminedPet = examination.ExaminedPet,
                Diagnosis = examination.Diagnosis,
                IsSick = examination.IsSick
            };
        }


        public virtual int EditExamination(int id, string diagnosis, bool isSick)
        {
            Examination theExamination = examinationRepository.One(id);
            theExamination.Diagnosis= diagnosis;
            theExamination.IsSick = isSick;
            theExamination.Date = DateTime.Now;
            unitOfWork.Commit();
            return theExamination.Id;
        }
    }
}

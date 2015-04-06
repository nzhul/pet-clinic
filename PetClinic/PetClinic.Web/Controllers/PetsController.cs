using PetClinic.Data.DomainObjects;
using PetClinic.Data.DomainObjects.Enums;
using PetClinic.Data.Infrastructure;
using PetClinic.Data.Repository;
using PetClinic.Data.Service;
using PetClinic.Web.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetClinic.Web.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetClinicService petClinicService;
        private readonly IRepository<Owner> ownerRepository;
        private readonly IRepository<Pet> petRepository;
        private readonly IRepository<Cat> catRepository;
        private readonly IRepository<Dog> dogRepository;
        private readonly IRepository<Bird> birdRepository;
        private readonly IRepository<Examination> examinationRepository;
        private readonly IUnitOfWork unitOfWork;

        public PetsController(IPetClinicService petClinicService, 
                              IRepository<Owner> ownerRepository,
                              IRepository<Pet> petRepository,
                              IRepository<Bird> birdRepository, 
                              IRepository<Cat> catRepository,
                              IRepository<Dog> dogRepository,
                              IRepository<Examination> examinationRepository,
                              IUnitOfWork unitOfWork)
        {
            this.petClinicService = petClinicService;
            this.ownerRepository = ownerRepository;
            this.petRepository = petRepository;
            this.catRepository = catRepository;
            this.dogRepository = dogRepository;
            this.birdRepository = birdRepository;
            this.examinationRepository = examinationRepository;
            this.unitOfWork = unitOfWork;
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            CreatePetForm model = new CreatePetForm();
            model.Owners = GetOwnersList();
            model.Types = GetTypesList();
            model.Genders = GetGendersList();
            model.Partners = GetPartnersList(0);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreatePetForm form)
        {
            int newPetId = 0;
            if (ModelState.IsValid)
            {
                switch (form.SelectedTypeId)
                {
                    case 1:
                        newPetId = petClinicService.CreateCat(form.Name, form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.NumberOfHoursSpentSleeping, form.FavouriteFood);
                        break;
                    case 2:
                        newPetId = petClinicService.CreateDog(form.Name, form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.FavouriteFood, form.FavouriteGame, form.IsAgressive);
                        break;
                    case 3:
                        newPetId = petClinicService.CreateBird(form.Name, form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.SelectedPartnerId);
                        break;
                    default:
                        break;
                }
                TempData["message"] = "Pet " + form.Name + ", was created successfully";
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }

            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
            form.Genders = GetGendersList();
            form.Partners = GetPartnersList(newPetId);
            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            Pet thePet = petRepository.One(id);
            CreatePetForm petForm = new CreatePetForm 
            {
                Age = thePet.Age,
                Breed = thePet.Breed,
                Name = thePet.Name,
                SelectedGenderId = thePet.Gender == Gender.Male ? 1 : 2,
                SelectedOwnerId = thePet.Owner.Id,
            };

            if (thePet is Cat)
            {
                Cat theCat = catRepository.One(thePet.Id);
                petForm.SelectedTypeId = 1;
                petForm.NumberOfHoursSpentSleeping = theCat.NumberOfHoursSpentSleeping;
                petForm.FavouriteFood = theCat.FavouriteFood;
            }
            if (thePet is Dog)
            {
                Dog theDog = dogRepository.One(thePet.Id);
                petForm.SelectedTypeId = 2;
                petForm.FavouriteFood = theDog.FavouriteFood;
                petForm.FavouriteGame = theDog.FavouriteGame;
                petForm.IsAgressive = theDog.IsAggressiveTowardsOtherPeople;
            }

            if (thePet is Bird)
            {
                Bird theBird = birdRepository.One(thePet.Id);
                petForm.SelectedTypeId = 3;
                if (theBird.Partner != null)
                {
                    petForm.SelectedPartnerId = theBird.Partner.Id;
                }
            }

            petForm.Owners = GetOwnersList();
            petForm.Types = GetTypesList();
            petForm.Genders = GetGendersList();
            petForm.Partners = GetPartnersList(id);
            return View(petForm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int id)
        {
            Pet thePet = petRepository.One(id);

            if (thePet is Bird)
            {
                Bird theBird = birdRepository.One(thePet.Id);
                if (theBird.Partner != null || theBird.PartnerId > 0)
                {
                    Bird thePartner = birdRepository.One(theBird.Partner.Id);
                    thePartner.Partner = null;
                    thePartner.PartnerId = 0;
                }
            }

            petRepository.Remove(thePet);

            IEnumerable<Examination> allExaminations = examinationRepository.All();

            foreach (var Examination in allExaminations)
            {
                if (Examination.ExaminedPetId == id)
                {
                    examinationRepository.Remove(Examination);
                }
            }

            unitOfWork.Commit();
            TempData["message"] = "The pet was deleted successfully";
            TempData["messageType"] = "danger";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CreatePetForm form)
        {
            if (ModelState.IsValid)
            {
                switch (form.SelectedTypeId)
                {
                    case 1: // Cat
                        Cat theCat = catRepository.One(form.Id);
                        theCat.Age = form.Age;
                        theCat.Breed = form.Breed;
                        theCat.Gender = form.SelectedGenderId == 1 ? Gender.Male : Gender.Female;
                        theCat.Name = form.Name;
                        theCat.OwnerId = form.SelectedOwnerId;
                        theCat.NumberOfHoursSpentSleeping = form.NumberOfHoursSpentSleeping;
                        theCat.FavouriteFood = form.FavouriteFood;
                        break;
                    case 2: // Dog
                        Dog theDog = dogRepository.One(form.Id);
                        theDog.Age = form.Age;
                        theDog.Breed = form.Breed;
                        theDog.Gender = form.SelectedGenderId == 1 ? Gender.Male : Gender.Female;
                        theDog.Name = form.Name;
                        theDog.OwnerId = form.SelectedOwnerId;
                        theDog.FavouriteFood = form.FavouriteFood;
                        theDog.FavouriteGame = form.FavouriteGame;
                        theDog.IsAggressiveTowardsOtherPeople = form.IsAgressive;
                        break;
                    case 3: // Bird
                        Bird partner = new Bird();
                        if (form.SelectedPartnerId == null)
                        {
                            partner = null;
                        }
                        else
                        {
                            partner = birdRepository.One((int)form.SelectedPartnerId);
                        }

                        Bird theBird = birdRepository.One(form.Id);
                        theBird.Age = form.Age;
                        theBird.Breed = form.Breed;
                        theBird.Gender = form.SelectedGenderId == 1 ? Gender.Male : Gender.Female;
                        theBird.Name = form.Name;
                        theBird.OwnerId = form.SelectedOwnerId;
                        theBird.Partner = partner;
                        break;
                    default:
                        break;
                }
                unitOfWork.Commit();
                TempData["message"] = "The pet was edited successfully";
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }

            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
            form.Genders = GetGendersList();
            form.Partners = GetPartnersList(form.Id);
            return View(form);
        }

        private IEnumerable<SelectListItem> GetOwnersList()
        {
            IEnumerable<SelectListItem> ownersList = 
                ownerRepository
                .All()
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString()
                });
            return new SelectList(ownersList, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetTypesList()
        {
            List<SelectListItem> typesList = new List<SelectListItem>();
            typesList.Add(new SelectListItem { Value = "1", Text = "Cat" });
            typesList.Add(new SelectListItem { Value = "2", Text = "Dog" });
            typesList.Add(new SelectListItem { Value = "3", Text = "Bird" });
            return new SelectList(typesList, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetGendersList()
        {
            List<SelectListItem> gendersList = new List<SelectListItem>();
            gendersList.Add(new SelectListItem { Value = "1", Text = "Male" });
            gendersList.Add(new SelectListItem { Value = "2", Text = "Female" });
            return new SelectList(gendersList, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetPartnersList(int petId)
        {
            IEnumerable<SelectListItem> partnersList =
                birdRepository
                .All()
                .OrderBy(x => x.Id)
                .Where(x =>x.Id != petId)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString()
                });
            return new SelectList(partnersList, "Value", "Text");
        }

    }
}
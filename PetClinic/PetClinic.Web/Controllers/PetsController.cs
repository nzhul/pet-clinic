using PetClinic.Data.DomainObjects;
using PetClinic.Data.DomainObjects.Enums;
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
        private readonly IRepository<Bird> birdRepository;

        public PetsController(IPetClinicService petClinicService, IRepository<Owner> ownerRepository, IRepository<Bird> birdRepository)
        {
            this.petClinicService = petClinicService;
            this.ownerRepository = ownerRepository;
            this.birdRepository = birdRepository;
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            CreatePetForm model = new CreatePetForm();
            model.Owners = GetOwnersList();
            model.Types = GetTypesList();
            model.Genders = GetGendersList();
            model.Partners = GetPartnersList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreatePetForm form)
        {
            if (ModelState.IsValid)
            {
                switch (form.SelectedTypeId)
                {
                    case 1:
                        petClinicService.CreateCat(form.Name, form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.NumberOfHoursSpentSleeping, form.FavouriteFood);
                        break;
                    case 2:
                        petClinicService.CreateDog(form.Name,form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.FavouriteFood, form.FavouriteGame, form.IsAgressive);
                        break;
                    case 3:
                        petClinicService.CreateBird(form.Name,form.SelectedOwnerId, form.Breed, form.Age, form.SelectedGenderId, form.SelectedPartnerId);
                        break;
                    default:
                        break;
                }
                return RedirectToAction("Index", "Home");
            }

            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
            form.Genders = GetGendersList();
            form.Partners = GetPartnersList();
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

        private IEnumerable<SelectListItem> GetPartnersList()
        {
            IEnumerable<SelectListItem> partnersList =
                birdRepository
                .All()
                .OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToString()
                });
            return new SelectList(partnersList, "Value", "Text");
        }

    }
}
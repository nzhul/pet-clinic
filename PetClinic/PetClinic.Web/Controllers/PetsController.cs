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

        public PetsController(IPetClinicService petClinicService, IRepository<Owner> ownerRepository)
        {
            this.petClinicService = petClinicService;
            this.ownerRepository = ownerRepository;
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            CreatePetForm model = new CreatePetForm();
            model.Owners = GetOwnersList();
            model.Types = GetTypesList();
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
                        petClinicService.CreateCat(form.Name, form.SelectedOwnerId, "Siamska", 12, Gender.Female, 33, "Fish");
                        break;
                    case 2:
                        //petClinicService.CreateDog();
                        break;
                    case 3:
                        //petClinicService.CreateBird();
                        break;
                    default:
                        break;
                }
                return RedirectToAction("Index", "Home");
            }

            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
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

    }
}
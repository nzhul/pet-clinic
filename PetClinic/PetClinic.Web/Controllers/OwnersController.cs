using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using PetClinic.Data.Service;
using PetClinic.Data.ViewModels;
using PetClinic.Web.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PetClinic.Web.Controllers
{
    public class OwnersController : Controller
    {

        private readonly IPetClinicService petClinicService;
        private readonly IRepository<Owner> ownerRepository;

        public OwnersController(IPetClinicService petClinicService, IRepository<Owner> ownerRepository)
        {
            this.petClinicService = petClinicService;
            this.ownerRepository = ownerRepository;
        }


        [HttpGet]
        [Authorize]
        public ActionResult Details(int id)
        {

            OwnerDetailsViewModel model = petClinicService.GetOwnerDetailsById(id);

            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateOwnerForm form)
        {
            if (ModelState.IsValid)
            {
                var createStatus = petClinicService.CreateOwner(form.Name);

                TempData["message"] = "Owner "+form.Name+", was created successfully";
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }
            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            Owner theOwner = ownerRepository.One(id);

            CreateOwnerForm ownerForm = new CreateOwnerForm 
            {
                Name = theOwner.Name,
                Pets = theOwner.Pets.Select(CreatePetViewModel)
            };

            return View(ownerForm);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CreateOwnerForm form)
        {
            if (ModelState.IsValid)
            {
                petClinicService.EditOwner(form.Id, form.Name);
                TempData["message"] = "The owner was edited successfully";
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }

            Owner theOwner = ownerRepository.One(form.Id);
            form.Pets = theOwner.Pets.Select(CreatePetViewModel);
            return View(form);
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
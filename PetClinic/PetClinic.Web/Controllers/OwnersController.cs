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

                //TODO: Use TempData
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
                Pets = theOwner.Pets.Select(x => new PetViewModel 
                                            {
                                                Age = x.Age,
                                                Breed = x.Breed,
                                                Gender = x.Gender,
                                                Id = x.Id,
                                                Name = x.Name,
                                                //TODO: Find a way to get pet type;
                                            })
            };

            return View(ownerForm);


        }
    }
}
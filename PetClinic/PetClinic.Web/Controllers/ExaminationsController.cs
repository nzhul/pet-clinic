using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetClinic.Web.InputModels;
using PetClinic.Data.DomainObjects;
using PetClinic.Data.Repository;
using PetClinic.Data.ViewModels;
using PetClinic.Data.Service;

namespace PetClinic.Web.Controllers
{
    public class ExaminationsController : Controller
    {
        private readonly IRepository<Owner> ownerRepository;
        private readonly IPetClinicService petClinicService;

        public ExaminationsController(IRepository<Owner> ownerRepository, IPetClinicService petClinicService)
        {
            this.ownerRepository = ownerRepository;
            this.petClinicService = petClinicService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult SearchPet()
        {
            SearchPetsForm form = new SearchPetsForm();
            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
            return View(form);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SearchPet(SearchPetsForm form)
        {
            if (ModelState.IsValid)
            {
                form.PetsResult = petClinicService.GetPetsFiltered(form.SelectedOwnerId, form.SelectedTypeId);
            }
            form.Owners = GetOwnersList();
            form.Types = GetTypesList();
            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Examine(int id)// The ID here is the Id pet
        {
            CreateExaminationForm form = new CreateExaminationForm();
            form.Pet = petClinicService.GetPetById(id);
            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id) // The ID here is the Id of the examination
        {
            //TODO: Finish Examination Editing
            CreateExaminationForm form = new CreateExaminationForm();
            form.Pet = petClinicService.GetPetById(id);
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
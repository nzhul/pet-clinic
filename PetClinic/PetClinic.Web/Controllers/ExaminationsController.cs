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
using PetClinic.Data.Infrastructure;

namespace PetClinic.Web.Controllers
{
    public class ExaminationsController : Controller
    {
        private readonly IRepository<Owner> ownerRepository;
        private readonly IRepository<Pet> petRepository;
        private readonly IRepository<Examination> examinationRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IPetClinicService petClinicService;

        public ExaminationsController(IRepository<Owner> ownerRepository,
                                      IRepository<Pet> petRepository, 
                                      IRepository<Examination> examinationRepository,
                                      IUnitOfWork unitOfWork,
                                      IPetClinicService petClinicService)
        {
            this.ownerRepository = ownerRepository;
            this.petRepository = petRepository;
            this.examinationRepository = examinationRepository;
            this.unitOfWork = unitOfWork;
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
            form.PetId = id;
            return View(form);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Examine(CreateExaminationForm form)
        {
            if (ModelState.IsValid)
            {
                Pet thePet = petRepository.One(form.PetId);
                Examination newExamination = new Examination
                {
                    ExaminedPet = thePet,
                    Date = DateTime.Now,
                    Diagnosis = form.Diagnosis,
                    IsSick = form.IsSick
                };

                examinationRepository.Add(newExamination);
                unitOfWork.Commit();

                TempData["message"] = "The Pet, was examined! at " + newExamination.Date;
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }

            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id) // The ID here is the Id of the examination
        {
            Examination examination = examinationRepository.One(id);
            CreateExaminationForm form = new CreateExaminationForm();
            form.Diagnosis = examination.Diagnosis;
            form.IsSick = examination.IsSick;
            form.Pet = petClinicService.GetPetById(id);
            return View(form);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(CreateExaminationForm form)
        {
            if (ModelState.IsValid)
            {
                petClinicService.EditExamination(form.Id, form.Diagnosis, form.IsSick);
                TempData["message"] = "Examination was edited successfully";
                TempData["messageType"] = "success";
                return RedirectToAction("Index", "Home");
            }

            form.Pet = petClinicService.GetPetById(form.PetId);
            return View(form);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Reports()
        {
            ReportsForm form = new ReportsForm();

            return View(form);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Reports(ReportsForm form)
        {
            if (form.SearchDate != null)
            {
                form.ExaminationsResult = petClinicService.GetExaminationsByDate(form.SearchDate.Value);
                return View(form);
            }
            ReportsForm newForm = new ReportsForm();
            newForm.SearchDate = form.SearchDate;
            return View(newForm);
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
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

        public OwnersController(IPetClinicService petClinicService)
        {
            this.petClinicService = petClinicService;
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
    }
}
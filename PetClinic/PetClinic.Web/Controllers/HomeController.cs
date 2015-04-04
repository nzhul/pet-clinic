using PetClinic.Data.Service;
using PetClinic.Data.ViewModels;
using PetClinic.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetClinic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPetClinicService petClinicService;

        public HomeController(IPetClinicService productService)
        {
            this.petClinicService = productService;
        }

        public ActionResult Index()
        {
            var model = new HomeViewModel();
            model.Pets = petClinicService.GetPets();
            model.Owners = petClinicService.GetOwners();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using PetClinic.Data.Service;
using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetClinic.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPetClinicService productService;

        public HomeController(IPetClinicService productService)
        {
            this.productService = productService;
        }

        public ActionResult Index()
        {
            //IEnumerable<CategoryViewModel> categories = productService.GetCategories();

            //int newCategoryId = productService.CreateCategory("FUCK");

            //string gosho = "12";

            return View();
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
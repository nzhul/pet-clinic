using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetClinic.Web.InputModels
{
    public class CreatePetForm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Owner:")]
        public int SelectedOwnerId { get; set; }
        public IEnumerable<SelectListItem> Owners { get; set; }


        [Required]
        [Display(Name = "Type:")]
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

    }
}
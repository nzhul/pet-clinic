using PetClinic.Data.DomainObjects;
using PetClinic.Data.DomainObjects.Enums;
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
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Invalid Name, Max: 150; Min: 3 Characters long")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Owner:")]
        public int SelectedOwnerId { get; set; }
        public IEnumerable<SelectListItem> Owners { get; set; }


        [Required]
        [Display(Name = "Pet Type:")]
        public int SelectedTypeId { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }

        [StringLength(150, MinimumLength = 3, ErrorMessage = "Invalid Input, Max: 150; Min: 3 Characters long")]
        public string Breed { get; set; }

        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender:")]
        public int SelectedGenderId { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }

        [Display(Name = "Number of hours spent sleeping:")]
        public int NumberOfHoursSpentSleeping { get; set; }

        [StringLength(150, MinimumLength = 3, ErrorMessage = "Invalid Input, Max: 150; Min: 3 Characters long")]
        [Display(Name = "Favourite Food:")]
        public string FavouriteFood { get; set; }

        [StringLength(150, MinimumLength = 3, ErrorMessage = "Invalid Input, Max: 150; Min: 3 Characters long")]
        [Display(Name = "Favourite Game:")]
        public string FavouriteGame { get; set; }

        [Display(Name = "Is Agressive towards other people:")]
        public bool IsAgressive { get; set; }

        [Display(Name = "Livetime partner:")]
        public int? SelectedPartnerId { get; set; }
        public IEnumerable<SelectListItem> Partners { get; set; }

    }
}
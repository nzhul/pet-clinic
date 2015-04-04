using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetClinic.Web.InputModels
{
    public class CreateOwnerForm
    {
        [Required]
        [DisplayName("Names")]
        public string Name { get; set; }

        public IEnumerable<PetViewModel> Pets { get; set; }

    }
}
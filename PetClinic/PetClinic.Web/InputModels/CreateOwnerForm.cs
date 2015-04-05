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
        public int Id { get; set; }

        [Required]
        [DisplayName("Names")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Invalid Names, Max: 150; Min: 3 Characters long")]
        public string Name { get; set; }

        public IEnumerable<PetViewModel> Pets { get; set; }

    }
}
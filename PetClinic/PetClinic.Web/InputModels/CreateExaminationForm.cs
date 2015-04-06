using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetClinic.Web.InputModels
{
    public class CreateExaminationForm
    {
        public int Id { get; set; }
        public int PetId { get; set; }

        public PetViewModel Pet { get; set; }

        [DataType(DataType.MultilineText)]
        public string Diagnosis { get; set; }

        [DisplayName("The pet is sick")]
        public bool IsSick { get; set; }
    }
}
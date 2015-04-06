using PetClinic.Data.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.ViewModels
{
    public class ExaminationViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Pet ExaminedPet { get; set; }

        public Owner PetOwner { get; set; }

        public string Diagnosis { get; set; }

        public bool IsSick { get; set; }

    }
}

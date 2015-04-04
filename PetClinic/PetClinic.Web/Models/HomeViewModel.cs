using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetClinic.Web.Models
{
    public class HomeViewModel
    {
        private IEnumerable<PetViewModel> pets;
        private IEnumerable<OwnerViewModel> owners;
        private IEnumerable<ExaminationViewModel> examinations;

        public HomeViewModel()
        {
            this.pets = new List<PetViewModel>();
            this.owners = new List<OwnerViewModel>();
            this.examinations = new List<ExaminationViewModel>();
        }


        public IEnumerable<PetViewModel> Pets
        {
            get { return this.pets; }
            set { this.pets = value; }
        }

        public IEnumerable<OwnerViewModel> Owners
        {
            get { return this.owners; }
            set { this.owners = value; }
        }

        public IEnumerable<ExaminationViewModel> Examinations
        {
            get { return this.examinations; }
            set { this.examinations = value; }
        }
    }
}
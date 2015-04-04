using PetClinic.Data.DomainObjects;
using PetClinic.Data.DomainObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.ViewModels
{
    public class PetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public Owner Owner { get; set; }

        public int OwnerId { get; set; }
    }
}

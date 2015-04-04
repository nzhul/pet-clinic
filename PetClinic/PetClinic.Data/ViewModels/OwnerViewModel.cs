using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.ViewModels
{
    public class OwnerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PetsCount { get; set; }
    }
}

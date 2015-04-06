using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Examination : IEntity
    {
        public virtual int Id { get; set; }

        public virtual string Diagnosis { get; set; }

        public virtual bool IsSick { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual int ExaminedPetId { get; set; }

        public virtual Pet ExaminedPet { get; set; }

    }
}

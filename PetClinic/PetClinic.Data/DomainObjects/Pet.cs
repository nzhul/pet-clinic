using PetClinic.Data.DomainObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Pet : IEntity, INamable
    {
        private IList<Examination> examinations = new List<Examination>();

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Breed { get; set; }

        public virtual int Age { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Owner Owner { get; set; }


        public virtual int OwnerId { get; set; }

        public virtual IList<Examination> Examinations
        {
            get
            {
                return this.examinations;
            }

            set
            {
                this.examinations = value;
            }
        }
    }
}

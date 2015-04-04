﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic.Data.DomainObjects
{
    public class Cat : Pet
    {
        public virtual int NumberOfHoursSpentSleeping { get; set; }

        public virtual string FavouriteFood { get; set; }
    }
}

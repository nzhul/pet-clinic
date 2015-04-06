using PetClinic.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetClinic.Web.InputModels
{
    public class ReportsForm
    {
        [DataType(DataType.Date)]
        public DateTime SearchDate {get;set;}
        public IEnumerable<ExaminationViewModel> ExaminationsResult { get; set; }
    }
}
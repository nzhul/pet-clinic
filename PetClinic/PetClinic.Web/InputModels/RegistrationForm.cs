using PetClinic.Web.Helpers;
using PetClinic.Web.Resources;
using PetClinic.Web.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetClinic.Web.InputModels
{
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessageResourceType = typeof(Errors), ErrorMessageResourceName = "PasswordsAreDifferent")]
    public class RegistrationForm
    {
        [Required]
        [DisplayName("User name")]
        [RegularExpression(ValidationHelper.UserNameRegularExpression, ErrorMessageResourceType = typeof(Errors), ErrorMessageResourceName = "InvalidUserName")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(ValidationHelper.EmailRegularExpression, ErrorMessageResourceType = typeof(Errors), ErrorMessageResourceName = "InvalidEmail")]
        [DisplayName("Email address")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
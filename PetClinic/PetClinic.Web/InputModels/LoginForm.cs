using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PetClinic.Web.Helpers;
using PetClinic.Web.Resources;

namespace PetClinic.Web.InputModels
{
    public class LoginForm
    {
        [Required]
        [DisplayName("User name")]
        [RegularExpression(ValidationHelper.UserNameRegularExpression, ErrorMessageResourceType = typeof(Errors), ErrorMessageResourceName = "InvalidUserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
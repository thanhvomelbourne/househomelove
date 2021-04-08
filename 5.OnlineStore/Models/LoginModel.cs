using OnlineStore.Helper;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = "Required")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessage), ErrorMessageResourceName = "Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
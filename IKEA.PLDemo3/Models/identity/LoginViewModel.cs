using System.ComponentModel.DataAnnotations;

namespace IKEA.PLDemo3.Models.identity
{
    public class LoginViewModel
    {


        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Display(Name = "RememberMe ")]

        public bool RememberMe { get; set; }
    }
}

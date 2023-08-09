using System.ComponentModel.DataAnnotations;

namespace ECPMaster.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string token { get; set; }
    }
}
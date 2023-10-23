using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
	public class ResetPassordVieModel
	{
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Confirm Password not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

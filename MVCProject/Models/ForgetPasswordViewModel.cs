using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
	public class ForgetPasswordViewModel
	{
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
    }
}

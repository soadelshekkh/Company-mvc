using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="Email is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required(ErrorMessage ="Password is required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage ="Confirm Password is required")]
		[Compare("Password",ErrorMessage ="Confirm Password not match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
		public string UserName { get; set; }
		public bool IsAgree { get; set; }
	}
}

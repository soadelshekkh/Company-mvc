using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage ="Email is Required")]
		[DataType(DataType.EmailAddress,ErrorMessage ="Enter Valid Mail")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		[Required(ErrorMessage ="Password is required")]
		public string PassWord { get; set; }
        [Required(ErrorMessage = "User name is required")]
        public string UserName { get; set; }
		public bool RemmeberMe { get; set; }
	}
}

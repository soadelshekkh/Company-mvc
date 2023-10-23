using DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace MVCProject.Helper
{
	public static class EmailSetting
	{
		public static void SendMail(Email email) 
        {
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("housefashion084@gmail.com", "lkyhzbiurftlvyei");
			client.Send("housefashion084@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}

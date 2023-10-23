﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class UserRegister:IdentityUser
	{
		public bool IsAgree { get; set; }

	}
}

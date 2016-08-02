using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace GNAR.Models
{
	public class PetUser : IdentityUser
	{
		public DateTime FirstFound { get; set; }
	}
}
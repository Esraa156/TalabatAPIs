using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository
{
	public static class ApplicationIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
		{

			if (!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{

					DisplayName = "Ahmed Nasr",
					Email = "ahmed.nasr@linkdev.com",
					UserName = "ahmed.nasr",
					PhoneNumber = "01123010703"
				};
				await userManager.CreateAsync(user, "Pass@word11");
			}
		}

	}
}
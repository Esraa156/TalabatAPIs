﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;

namespace TalabatProject.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost("login")] //POST: /api/Account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto model)

		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				return Unauthorized(new ApiResponse(401, "Invalid Login"));

			}
			else
			{
				var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
				if (!Result.Succeeded)
				{
					return Unauthorized(new ApiResponse(401, "Invalid Login"));

				}
				else
				{
					return Ok(new UserDto
					{
						DisplayName = user.DisplayName,
						Email = user.Email,
						Token = "This will be token"


					});
				}
			}
		}
		[HttpPost("register")] //POST: /api/Account/register

		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var user = new ApplicationUser()
			{
				DisplayName = model.DispalyName,
				Email = model.Email,
				UserName = model.Email.Split("@")[0],
				PhoneNumber = model.Phone
			};
			var result=await _userManager.CreateAsync(user,model.Password);
			if (!result.Succeeded)
			
				return BadRequest(new ApiValidationErrorResponse(){Errors=result.Errors.Select(E=>E.Description)});

			
				return Ok(new UserDto
				{
					DisplayName = user.DisplayName,
					Email = user.Email,
					Token = "This will be token"


				});
			}
		}


	}
	
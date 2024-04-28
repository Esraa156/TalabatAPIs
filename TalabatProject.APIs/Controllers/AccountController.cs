using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using TalabatProject.APIs.DTOs;
using TalabatProject.APIs.Errors;
using TalabatProject.APIs.Extensions;

namespace TalabatProject.APIs.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IAuthService authService,IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
			_mapper = mapper;
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
						Token = await _authService.CreateTokenAsync(user, _userManager)


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
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded)

				return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E => E.Description) });


			return Ok(new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)


			});
		}

		[Authorize]
		[HttpGet] //GET : /api/Account
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);

			return Ok(new UserDto()
			{

				DisplayName = user?.DisplayName ?? string.Empty,
				Email = user?.Email ?? string.Empty,
				Token = await _authService.CreateTokenAsync(user, _userManager)



			});
		}


		[Authorize]
		[HttpGet("address")] //GET : /api/Account/Address
		public async Task<ActionResult<AddressDto>> GetUserAddress()
		{
			var user = await _userManager.FindUserWithAddress(User);


			return Ok(_mapper.Map<AddressDto>(user.Address));

		}

		[Authorize]
		[HttpPut("address")] //PUT : /api/Account/Address
		public async Task<ActionResult<Address>> UpdateUserAddress(AddressDto address)
		{
			var UpdatedAddress =_mapper.Map<Address>(address);
			var user=await _userManager.FindUserWithAddress(User);


			UpdatedAddress.Id=user.Address.Id;
			user.Address = UpdatedAddress;

			var result=await _userManager.UpdateAsync(user);
			if(!result.Succeeded)
			{
				return BadRequest(new ApiValidationErrorResponse()
				{
					Errors = result.Errors.Select(E => E.Description)

				});
			}
			return Ok(address);

		}







	}

}
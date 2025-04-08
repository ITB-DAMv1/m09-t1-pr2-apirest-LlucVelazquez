using ClientWebRP.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerWebAPI.Model;

namespace ServerWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<AuthController> _logger;
		private readonly IConfiguration _configuration;
		public AuthController(UserManager<ApplicationUser> userManager, ILogger<AuthController> logger, IConfiguration configuration)
		{
			_userManager = userManager;
			_logger = logger;
			_configuration = configuration;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO model)
		{
			var user = new ApplicationUser
			{
				UserName = model.UserName,
				Email = model.Email,
				Name = model.Name
			};
			if (model.Password != model.PasswordConfirmed)
			{
				return BadRequest("Passwords do not match");
			}
			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded)
				return Ok("Usuari registrat");

			return BadRequest(result.Errors);
		}
		//[HttpPost("admin/register")]

	}
}

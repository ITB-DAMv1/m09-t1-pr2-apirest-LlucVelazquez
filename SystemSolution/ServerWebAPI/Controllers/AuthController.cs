using ClientWebRP.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerWebAPI.DTOs;
using System.Security.Claims;
using ServerWebAPI.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

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
		[HttpPost("admin/register")]
		public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDTO model)
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
			var roleResult = new IdentityResult();
			if (result.Succeeded)
			{
				roleResult = await _userManager.AddToRoleAsync(user, "Admin");
			}
			if (result.Succeeded && roleResult.Succeeded)
			{
				return Ok("Usuari registrat");
			}
			return BadRequest(result.Errors);
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
			{
				return Unauthorized("Invalid credentials");
			}
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id)
			};
			var roles = await _userManager.GetRolesAsync(user);
			if (roles != null && roles.Count > 0)
			{
				foreach (var role in roles)
				{
					claims.Add(new Claim(ClaimTypes.Role, role));
				}
			}
			return Ok( CreateToken(claims.ToArray()));
		}
		private string CreateToken(Claim[] claims)
		{
			var jwtConfig = _configuration.GetSection("JwtSettings");
			var secretKey = jwtConfig["Key"];
			var issuer = jwtConfig["Issuer"];
			var audience = jwtConfig["Audience"];
			var expirationMinutes = int.Parse(jwtConfig["ExpirationMinutes"]);
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				issuer: issuer,
				audience: audience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(expirationMinutes),
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

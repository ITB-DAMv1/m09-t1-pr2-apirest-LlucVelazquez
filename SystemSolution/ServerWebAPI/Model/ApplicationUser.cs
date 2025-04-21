using Microsoft.AspNetCore.Identity;

namespace ServerWebAPI.Model
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public List<Vote>? Votes { get; set; }
	}
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerWebAPI.Model;

namespace ServerWebAPI.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Game> Games { get; set; }
		public DbSet<Vote> Votes { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<Game>()
				.HasMany(g => g.Votes)
				.WithOne(v => v.Game)
				.HasForeignKey(v => v.GameId)
				.OnDelete(DeleteBehavior.Cascade);
			builder.Entity<ApplicationUser>()
				.HasMany(g => g.Votes)
				.WithOne(v => v.User)
				.HasForeignKey(v => v.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
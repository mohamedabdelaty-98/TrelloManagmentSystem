using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem.Data
{
	public class Context : IdentityDbContext<ApplicationUser>
	{
		public Context() { }
		public Context(DbContextOptions<Context> options) : base(options)
		{
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
		}

		public DbSet<Tasks> Tacks { get; set; }
		public DbSet<Project> Projects { get; set; }

 
	}
}

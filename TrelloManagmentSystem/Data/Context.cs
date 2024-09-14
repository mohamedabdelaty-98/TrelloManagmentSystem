using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrelloManagmentSystem.Models;

namespace TrelloManagmentSystem.Data
{
    public class Context : IdentityDbContext<AppUser>
    {
        public Context() 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }
        public Context(DbContextOptions<Context> options):base(options)
        {
            
        }
        public DbSet<Tasks> Tasks { get; set; }
		public DbSet<Project> Project { get; set; }
        //public int MyProperty { get; set; }

    }

}

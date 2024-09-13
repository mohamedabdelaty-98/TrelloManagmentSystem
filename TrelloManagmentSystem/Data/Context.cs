using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
    }
}

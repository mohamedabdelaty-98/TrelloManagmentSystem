using Microsoft.EntityFrameworkCore;

namespace TrelloManagmentSystem.Data
{
    public class Context: DbContext
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

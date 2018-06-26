using Microsoft.EntityFrameworkCore;
 
namespace activity_belt_demo.Models
{
    public class dbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<ActivityPlan> ActivityPlans {get; set;}

        public DbSet<UserActivityPlan> UserActivityPlans {get; set;}
        
        
        // base() calls the parent class' constructor passing the "options" parameter along
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }
    }
}

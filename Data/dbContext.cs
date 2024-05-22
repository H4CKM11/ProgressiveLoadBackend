using Microsoft.EntityFrameworkCore;

namespace ProgressiveLoadBackend.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<Models.Users> Users { get; set; }
    }
 
}

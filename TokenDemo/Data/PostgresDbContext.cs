
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TokenDemo.Data
{
    public class PostgresDbContext : IdentityDbContext<ApplicationUser>
    {
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {            
            base.OnModelCreating(builder);
            
            builder.HasPostgresExtension("adminpack");
            builder.HasDefaultSchema("public");
        }
    }
}

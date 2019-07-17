using Microsoft.EntityFrameworkCore;

namespace Test.NoAuth.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<NoAuthDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for NoAuthDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}

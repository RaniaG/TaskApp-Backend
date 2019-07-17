using Test.NoAuth.Configuration;
using Test.NoAuth.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Test.NoAuth.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class NoAuthDbContextFactory : IDesignTimeDbContextFactory<NoAuthDbContext>
    {
        public NoAuthDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NoAuthDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(NoAuthConsts.ConnectionStringName)
            );

            return new NoAuthDbContext(builder.Options);
        }
    }
}
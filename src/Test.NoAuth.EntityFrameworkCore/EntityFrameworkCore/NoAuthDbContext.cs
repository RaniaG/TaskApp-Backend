using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth.EntityFrameworkCore
{
    
    public class NoAuthDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public virtual DbSet<TaskItem> Tasks { get; set; }
        public NoAuthDbContext(DbContextOptions<NoAuthDbContext> options) 
            : base(options)
        {

        }
    }
}

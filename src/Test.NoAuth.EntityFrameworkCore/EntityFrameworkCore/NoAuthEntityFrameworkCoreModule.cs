using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.MicroKernel.Registration;
using Microsoft.EntityFrameworkCore;


namespace Test.NoAuth.EntityFrameworkCore
{
    [DependsOn(
        typeof(NoAuthCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class NoAuthEntityFrameworkCoreModule : AbpModule
    {

        public override void PreInitialize()
        {
            //To Fix this error
            /*
              Can't create component 'Test.NoAuth.EntityFrameworkCore.NoAuthDbContext' as it has dependencies to be satisfied.
             */
            Configuration.IocManager.IocContainer.Register(Component
            .For(typeof(DbContextOptions<NoAuthDbContext>)).UsingFactoryMethod((kernel, context) =>
            {
                var builder = new DbContextOptionsBuilder<NoAuthDbContext>();
                builder.UseSqlServer(Configuration.DefaultNameOrConnectionString);
                return builder.Options;
            }));
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
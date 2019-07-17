using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.NoAuth.Web.Startup;
namespace Test.NoAuth.Web.Tests
{
    [DependsOn(
        typeof(NoAuthWebModule),
        typeof(AbpAspNetCoreTestBaseModule)
        )]
    public class NoAuthWebTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthWebTestModule).GetAssembly());
        }
    }
}
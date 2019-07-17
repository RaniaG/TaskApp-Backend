using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Test.NoAuth.EntityFrameworkCore
{
    [DependsOn(
        typeof(NoAuthCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class NoAuthEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
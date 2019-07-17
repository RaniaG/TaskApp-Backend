using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Test.NoAuth
{
    [DependsOn(
        typeof(NoAuthCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class NoAuthApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthApplicationModule).GetAssembly());
        }
    }
}
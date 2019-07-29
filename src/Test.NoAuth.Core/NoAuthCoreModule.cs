using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.NoAuth.Localization;

namespace Test.NoAuth
{
    public class NoAuthCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            NoAuthLocalizationConfigurer.Configure(Configuration.Localization);


            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthCoreModule).GetAssembly());
        }
    }
}
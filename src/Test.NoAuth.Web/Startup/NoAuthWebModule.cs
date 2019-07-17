using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.NoAuth.Configuration;
using Test.NoAuth.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Test.NoAuth.Web.Startup
{
    [DependsOn(
        typeof(NoAuthApplicationModule), 
        typeof(NoAuthEntityFrameworkCoreModule), 
        typeof(AbpAspNetCoreModule))]
    public class NoAuthWebModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public NoAuthWebModule(IHostingEnvironment env)
        {
            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(NoAuthConsts.ConnectionStringName);

            Configuration.Navigation.Providers.Add<NoAuthNavigationProvider>();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(NoAuthApplicationModule).GetAssembly()
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthWebModule).GetAssembly());
        }
    }
}
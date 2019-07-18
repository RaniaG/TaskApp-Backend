using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Test.NoAuth.DTOs;
using Test.NoAuth.TaskBC;

namespace Test.NoAuth
{
    [DependsOn(
        typeof(NoAuthCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class NoAuthApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                //mapping from CreateTaskItemDTOInput to taskitem
                config.CreateMap<CreateTaskItemDTOInput, TaskItem>()
                      .ForMember(t => t.CreationTime, options => options.Ignore())
                      .ForMember(t => t.IsDeleted, options => options.Ignore())
                      .ForMember(t => t.Id, options => options.Ignore())
                      .ForMember(t => t.Status, options => options.Ignore());

            });
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(NoAuthApplicationModule).GetAssembly());
        }
    }
}
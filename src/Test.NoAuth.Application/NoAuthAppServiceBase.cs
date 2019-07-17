using Abp.Application.Services;

namespace Test.NoAuth
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class NoAuthAppServiceBase : ApplicationService
    {
        protected NoAuthAppServiceBase()
        {
            LocalizationSourceName = NoAuthConsts.LocalizationSourceName;
        }
    }
}
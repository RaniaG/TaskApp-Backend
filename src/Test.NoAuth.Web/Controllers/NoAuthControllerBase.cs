using Abp.AspNetCore.Mvc.Controllers;

namespace Test.NoAuth.Web.Controllers
{
    public abstract class NoAuthControllerBase: AbpController
    {
        protected NoAuthControllerBase()
        {
            LocalizationSourceName = NoAuthConsts.LocalizationSourceName;
        }
    }
}
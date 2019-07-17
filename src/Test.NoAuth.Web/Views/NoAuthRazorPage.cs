using Abp.AspNetCore.Mvc.Views;

namespace Test.NoAuth.Web.Views
{
    public abstract class NoAuthRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected NoAuthRazorPage()
        {
            LocalizationSourceName = NoAuthConsts.LocalizationSourceName;
        }
    }
}

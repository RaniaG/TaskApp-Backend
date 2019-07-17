using System;
using System.Threading.Tasks;
using Abp.TestBase;
using Test.NoAuth.EntityFrameworkCore;
using Test.NoAuth.Tests.TestDatas;

namespace Test.NoAuth.Tests
{
    public class NoAuthTestBase : AbpIntegratedTestBase<NoAuthTestModule>
    {
        public NoAuthTestBase()
        {
            UsingDbContext(context => new TestDataBuilder(context).Build());
        }

        protected virtual void UsingDbContext(Action<NoAuthDbContext> action)
        {
            using (var context = LocalIocManager.Resolve<NoAuthDbContext>())
            {
                action(context);
                context.SaveChanges();
            }
        }

        protected virtual T UsingDbContext<T>(Func<NoAuthDbContext, T> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<NoAuthDbContext>())
            {
                result = func(context);
                context.SaveChanges();
            }

            return result;
        }

        protected virtual async Task UsingDbContextAsync(Func<NoAuthDbContext, Task> action)
        {
            using (var context = LocalIocManager.Resolve<NoAuthDbContext>())
            {
                await action(context);
                await context.SaveChangesAsync(true);
            }
        }

        protected virtual async Task<T> UsingDbContextAsync<T>(Func<NoAuthDbContext, Task<T>> func)
        {
            T result;

            using (var context = LocalIocManager.Resolve<NoAuthDbContext>())
            {
                result = await func(context);
                context.SaveChanges();
            }

            return result;
        }
    }
}

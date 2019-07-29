using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Abp.EntityFrameworkCore;
using Test.NoAuth.EntityFrameworkCore;
using Castle.Facilities.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Filters;
using Test.NoAuth.Web.Hangfire.Auth;
using Hangfire.Dashboard;
using Test.NoAuth.ApplicationServices;
using Hangfire.Logging;
using Test.NoAuth.Web.Hangfire.CustomLog;
using Test.NoAuth.Web.Middlewares;

namespace Test.NoAuth.Web.Startup
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //Configure DbContext
            services.AddAbpDbContext<NoAuthDbContext>(options =>
            {
                DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
            });
            //returns null!!! //connection string must be defined in app.config or web.config
            string conn=Configuration.GetConnectionString("HangfireConnection");


            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                //.UseSqlServerStorage("HangfireConnection", new SqlServerStorageOptions
                .UseSqlServerStorage("Server=.;Database=HangfireTest;Integrated Security=True;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromSeconds(60),
                    PrepareSchemaIfNecessary = false, //wont recreate the db if it doesnt exist
                    //QueuePollInterval = TimeSpan.Zero, //time for polling
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.ReturnHttpNotAcceptable = true;
                //the following line for xml formating and must download its package
                //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            //Configure Abp and Dependency Injection
            return services.AddAbp<NoAuthWebModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        public void Configure(IApplicationBuilder app, IBackgroundJobClient backgroundJobs,  IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            //custom middleware
            app.UseMyMiddleware();


            app.UseStaticFiles();

            //setup custom logger for hangfire
            LogProvider.SetCurrentLogProvider(new CustomLogProvider());
            app.UseHangfireServer();
            //Authorization filters for hangfire dashboard
            app.UseHangfireDashboard("/hangfire",new DashboardOptions() {
                Authorization =new[] {new HangfireAuthFilter() },
                //IsReadOnlyFunc = (DashboardContext context) => true, //make dashboard readonly
                //AppPath = "http://your-app.net" //back to app button config
            });

            app.UseAbp(); //Initializes ABP framework.

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //delete tasks which have been soft deleted for more than 30 days
            RecurringJob.AddOrUpdate<ITaskAppService>("HDRJ", x => x.HardDeleteTasks(), Cron.Daily);
        }
    }
}

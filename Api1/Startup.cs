using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommonLib.Sql;
using static Api1.Services.SubscriberService;
using Api1.Services;
using Api1.EfDbContext;
using Microsoft.EntityFrameworkCore;
using Api1.AOP;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace Api1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType<AspectEventAutoCommit>();
            builder.RegisterType<AppService>().As<IAppService>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(AspectEventAutoCommit));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string connName = "Default";
            string connString = Configuration.GetConnectionString(connName);
            services.AddDbContext<AppDbContext>(x=>x.UseSqlServer(connString));
            
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddScoped<ICapPublishContext, CapPublishContext>();
            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();
                x.UseRabbitMQ(o => {
                    o.HostName = "192.168.56.10";
                    o.UserName = "rabbit";
                    o.Password = "rabbit";
                });
                x.UseDashboard(); // Register Dashboard http://localhost:5000/cap
            });
            services.AddControllers();
            services.AddTransient<AspectEventAutoCommit>();
            services.AddScoped<ICapPublishContext, CapPublishContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

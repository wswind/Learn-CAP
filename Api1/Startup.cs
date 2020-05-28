using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommonLib.Sql;
using static Api1.Services.SubscriberService;
using Api1.Services;
using Api1.EfDbContext;
using Microsoft.EntityFrameworkCore;

namespace Api1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            const string connName = "Default";
       
            services.AddSqlFactoryBuilder().AddSingleSqlFactory("Default");
            services.AddTransient<ISubscriberService, SubscriberService>();

            string connString = Configuration.GetConnectionString(connName);
            services.AddDbContext<AppDbContext>(x=>x.UseSqlServer(connString));

            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();
                // If you are using ADO.NET, choose to add configuration you needed：
                x.UseSqlServer(connString);
                x.UseRabbitMQ(o => {
                    o.HostName = "192.168.56.10";
                    o.UserName = "rabbit";
                    o.Password = "rabbit";
                });
                // Register Dashboard http://localhost:5000/cap
                x.UseDashboard();
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

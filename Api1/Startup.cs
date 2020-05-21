using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommonLib.Sql;
using static Api1.Services.SubscriberService;
using Api1.Services;

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
            services.AddSqlFactoryBuilder().AddSingleSqlFactory("Default");
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(x =>
            {
                // If you are using ADO.NET, choose to add configuration you needed：
                x.UseSqlServer("Server=.;Database=BFF;Trusted_Connection=True;MultipleActiveResultSets=true");
                x.UseRabbitMQ(o => {
                    o.HostName = "192.168.56.10";
                    o.UserName = "rabbit";
                    o.Password = "rabbit";
                });
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

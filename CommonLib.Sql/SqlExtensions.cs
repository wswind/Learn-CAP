using Microsoft.Extensions.DependencyInjection;

namespace CommonLib.Sql
{
    public static class SqlExtensions
    {
        public static IServiceCollection AddSqlFactoryBuilder(this IServiceCollection services)
        {
            services.AddSingleton<ISqlFactoryBuilder, SqlFactoryBuilder>();
            return services;
        }

        public static IServiceCollection AddSingleSqlFactory(this IServiceCollection services, string connName)
        {
            services.AddSingleton((sp) =>
            {
                var maker = sp.GetRequiredService<ISqlFactoryBuilder>();
                return maker.GetSqlFactory(connName);
            });
            return services;
        }
    }
}

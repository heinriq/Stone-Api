using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoneApi.Business;
using StoneApi.DAL;

namespace StoneApi.Tests{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<TransactionContext>(
                    options => options.UseSqlServer(
                        "Data Source=.\\SQLEXPRESS;Initial Catalog=DevDB;Integrated Security=True;MultipleActiveResultSets=True"
                    )
                );            
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionBusiness, TransactionBusiness>();             
        }
    }
}
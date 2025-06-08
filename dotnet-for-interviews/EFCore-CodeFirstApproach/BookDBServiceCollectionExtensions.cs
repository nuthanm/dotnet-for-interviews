using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCore_CodeFirstApproach
{
    public static class BookDBServiceCollectionExtensions
    {
        public static IServiceCollection AddBookDBContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BookDBContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}

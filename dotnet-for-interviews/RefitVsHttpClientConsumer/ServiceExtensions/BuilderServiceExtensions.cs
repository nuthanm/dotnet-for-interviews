using Microsoft.Extensions.DependencyInjection;
using Refit;
using RefitVsHttpClientConsumer.Contracts;

namespace RefitVsHttpClientConsumer.ServiceExtensions
{
    public static class BuilderServiceExtensions
    {
        public static IServiceCollection RegisterRefitClient(this IServiceCollection services)
        {
            services.AddRefitClient<IUserMangementService>().ConfigureHttpClient(
            c =>
            {
                c.BaseAddress = new Uri("http://localhost:7042/api");
            });

            return services;
        }
    }
}

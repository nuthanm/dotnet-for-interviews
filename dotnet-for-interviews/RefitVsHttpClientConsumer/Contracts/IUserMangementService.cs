using Refit;
using RefitVsHttpClientProducer.Models;

namespace RefitVsHttpClientConsumer.Contracts
{
    public interface IUserMangementService
    {
        [Get("/getUser/{id}")]
        Task<User> GetUser(int id);
    }
}

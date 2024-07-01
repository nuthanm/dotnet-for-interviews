using DemoOnRefit.Models;
using Refit;

namespace DemoOnRefit.Contracts
{
    public interface IUserManagement
    {
        [Get("/users")]
        Task<IEnumerable<User>> GetUsers();

        [Get("/users/{id}")]
        Task<User> GetUser(int id);

        [Post("/users")]
        Task<User> CreateUser([Body] User user);

        [Put("/users/{id}")]
        Task<User> UpdateUser(int id, [Body] User user);

        [Delete("/users/{id}")]
        Task DeleteUser(int id);
    }
}

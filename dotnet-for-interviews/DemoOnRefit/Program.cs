using DemoOnRefit.Contracts;
using DemoOnRefit.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services
        .AddRefitClient<IUserManagement>()
        .ConfigureHttpClient(config => config.BaseAddress = new Uri("https://jsonplaceholder.typicode.com"));

    }).Build();


#region ReadAllUsers
var userClient = host.Services.GetRequiredService<IUserManagement>();
var users = await userClient.GetUsers();

foreach (var userDetail in users)
{
    Console.WriteLine(userDetail.Name);
}
#endregion

#region GetUser

var user = await userClient.GetUser(1);
Console.WriteLine(user.Name);

#endregion

#region CreateUser
var newUser = new User { Name = "Nani" };
var createdUser = await userClient.CreateUser(newUser);
Console.WriteLine($"User with id: {createdUser.Id} and its name: {createdUser.Name}");
#endregion

#region UpdateUser
var existingUser = await userClient.GetUser(1);
existingUser.Name = "Sree";

var updatedUser = await userClient.UpdateUser(existingUser.Id, existingUser);
Console.WriteLine($"User with id; {updatedUser.Id} has changed the name: {updatedUser.Name}");
#endregion

#region DeleteUser
await userClient.DeleteUser(createdUser.Id);
#endregion
using MoviesProject.Models;

namespace MoviesProject.Services.Users
{
    public interface IUserAPIcall
    {
        Task<List<UserModel>> GetUsers();
        Task CreateUserAsync(string nombre, string apellido, string email, CancellationToken cancellation = default);
    }
}
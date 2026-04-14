using MoviesProject.Models;
using MoviesProject.Services.Users;

namespace MoviesProject.Views
{
    public class UserApiCall : IUserAPIcall
    {

        private readonly HttpClient _conexion;

        public UserApiCall(HttpClient conexion) => _conexion = conexion;

        public async Task<List<UserModel>> GetUsers()
        {
            var response = await _conexion.GetAsync("api/User");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                return users ?? new List<UserModel>();
            }
            else
            {
                throw new Exception($"Error al obtener usuarios: {response.ReasonPhrase}");
            }
        }
        public async Task CreateUserAsync(string Nombre, string Apellido, string email, CancellationToken cancellation = default)
        {
            var newUser = new UserModel
            {
                Nombre = Nombre,
                Apellido = Apellido,
                Email = email
            };
            var response = await _conexion.PostAsJsonAsync("api/User", newUser, cancellation);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al crear usuario: {response.ReasonPhrase}");
            }
        }
    }
}

using System.Net.Http.Json;
using MoviesProject.Models;

namespace MoviesProject.Services.Users
{
    public class UserApiCall : IUserAPIcall
    {
        private readonly HttpClient _conexion;

        public UserApiCall(HttpClient conexion)
        {
            _conexion = conexion;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            try
            {
                var response = await _conexion.GetAsync("api/User");

                if (response.IsSuccessStatusCode)
                {
                    var users = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                    return users ?? new List<UserModel>();
                }

                return new List<UserModel>();
            }
            catch (Exception ex)
            {
                return new List<UserModel>();
            }
        }

        public async Task CreateUserAsync(string nombre, string apellido, string email, CancellationToken cancellation = default)
        {
            var newUser = new UserModel
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email
            };

            var response = await _conexion.PostAsJsonAsync("api/User", newUser, cancellation);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync(cancellation);
                throw new Exception($"Error al crear usuario en la API: {response.StatusCode} - {errorMsg}");
            }
        }
    }
}
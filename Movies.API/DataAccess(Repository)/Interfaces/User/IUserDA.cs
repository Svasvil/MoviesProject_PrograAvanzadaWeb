using Movies.API.Models;

namespace Movies.API.DataAccess_Repository_.Interfaces.User
{
    public interface IUserDA
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> AddUser(UserModel newUser);
    }
}

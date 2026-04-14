using Movies.API.DTOs.Users;
using Movies.API.Models;

namespace Movies.API.BussinessLogic_Services_.Interfaces.User
{
    public interface IUserBL
    {
        Task<List<UserDTO>> GetUsers();
        Task<UserDTO> AddUser(UserModel model);
    }
}

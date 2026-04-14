using Movies.API.BussinessLogic_Services_.Interfaces.User;
using Movies.API.DataAccess_Repository_.Interfaces.User;
using Movies.API.DTOs.Users;
using Movies.API.Models;

namespace Movies.API.BussinessLogic_Services_.Logic.User
{
    public class UserBL: IUserBL
    {
        private readonly IUserDA _User;
        public UserBL(IUserDA user)
        {
            _User = user;
        }
        //get all users 

        public async Task<List<UserDTO>> GetUsers()
        {
            var users = await _User.GetUsers();
            return users.Select(u => new UserDTO(
                u.Id,
                u.Nombre,
                u.Apellido,
                u.Email
                )).ToList();
        }


        //add user 
        public async Task<UserDTO> AddUser(UserModel model)
        {
            var newUser = await _User.AddUser(model);
            return new UserDTO(
                newUser.Id,
                newUser.Nombre,
                newUser.Apellido,
                newUser.Email
            );
        }
    }
}

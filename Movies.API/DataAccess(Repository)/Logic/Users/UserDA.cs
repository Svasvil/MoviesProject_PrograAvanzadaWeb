using Microsoft.EntityFrameworkCore;
using Movies.API.DataAccess_Repository_.Interfaces.User;
using Movies.API.DatabasesConnections;
using Movies.API.Models;

namespace Movies.API.DataAccess_Repository_.Logic.Users
{
    public class UserDA : IUserDA
    {
        private readonly ObjContex _Contexto;
        public UserDA(ObjContex Contexto) => _Contexto = Contexto;

        //get all
        public async Task<List<UserModel>> GetUsers() =>
                await _Contexto.User.Select(u => new UserModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email
                }).ToListAsync();


    
    //add user 
      public async Task<UserModel> AddUser(UserModel newUser)
        {
            _Contexto.User.Add(newUser);
            await _Contexto.SaveChangesAsync();
            return newUser;
        }

    }
}

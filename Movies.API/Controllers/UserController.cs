using Microsoft.AspNetCore.Mvc;
using Movies.API.Models;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BussinessLogic_Services_.Interfaces.User.IUserBL _userBL;
        public UserController(BussinessLogic_Services_.Interfaces.User.IUserBL userBL)
        {
            _userBL = userBL;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userBL.GetUsers();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Models.UserModel user)
        {
            if (user == null) return BadRequest();
            var newUser = await _userBL.AddUser(user);
            return Ok(newUser);
        }
    }
}

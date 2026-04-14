using Microsoft.AspNetCore.Mvc;
using MoviesProject.Models;
using MoviesProject.Services.Users;

namespace MoviesProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAPIcall _userApiCall;

        public UserController(IUserAPIcall userApiCall)
        {
            _userApiCall = userApiCall;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _userApiCall.GetUsers();
            return View(userList.OrderBy(u => u.Id));
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                await _userApiCall.CreateUserAsync(model.Nombre, model.Apellido, model.Email);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

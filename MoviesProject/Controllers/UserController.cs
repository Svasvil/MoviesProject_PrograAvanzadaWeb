using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email)
        {
            var usuarios = await _userApiCall.GetUsers();
            var usuarioEncontrado = usuarios.FirstOrDefault(u => u.Email.Trim().ToLower() == email.Trim().ToLower());

            if (usuarioEncontrado != null)
            {
                TempData["UserLogged"] = usuarioEncontrado.Nombre;


                TempData.Keep("UserLogged");

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "El correo no coincide con ningún usuario registrado.";
            return View();
        }

        public IActionResult Logout()
        {
            TempData.Remove("UserLogged");
            return RedirectToAction("Login");
        }

      
        public async Task<IActionResult> Index()
        {
            var userList = await _userApiCall.GetUsers();
            return View(userList.OrderBy(u => u.Id));
        }
    }
}
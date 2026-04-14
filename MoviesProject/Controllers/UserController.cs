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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (email == "admin@retro.com" && password == "admin123")
            {
                TempData["UserLogged"] = "ADMIN MASTER";
                TempData.Keep("UserLogged");
                return RedirectToAction("Index", "Home");
            }

            var usuarios = await _userApiCall.GetUsers();

            var usuarioEncontrado = usuarios.FirstOrDefault(u =>
                u.Email.Trim().ToLower() == email.Trim().ToLower());

            if (usuarioEncontrado != null)
            {
                TempData["UserLogged"] = usuarioEncontrado.Nombre;
                TempData.Keep("UserLogged");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "CREDENCIALES INVÁLIDAS";
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
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            try
            {
                if (model == null)
                {
                    TempData["Error"] = "Los datos del usuario son inválidos.";
                    return RedirectToAction("Index");
                }

                await _userApiCall.CreateUserAsync(model.Nombre, model.Apellido, model.Email);

                TempData["Success"] = "Usuario creado correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo crear el usuario: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
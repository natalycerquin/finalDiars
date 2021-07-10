using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using N00019639.DB;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace N00019639.Controllers
{
    public class AuthController : Controller
    {
        private AppYapeContext context;

        public AuthController(AppYapeContext context)
        {
            this.context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var usuario = context.Usuarios.FirstOrDefault(o => o.Username == Username && o.Password == CreateHash(Password));
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, Username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario usuario)
        {
            var usuarioBd = context.Usuarios.FirstOrDefault(o => o.Username.Equals(usuario.Username) && o.Password.Equals(usuario.Password));
            if (usuario != null)
            {
                usuario.Password = CreateHash(usuario.Password);
                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
            return RedirectToAction();
        }
        private string CreateHash(string input)
        {
            input += "12344567abcedef";
            var sha = SHA512.Create();

            var bytes = Encoding.Default.GetBytes(input);
            var hash = sha.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

    }
}

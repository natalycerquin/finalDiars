using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using N00019639.DB;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {
        private AppYapeContext context;

        public CuentaController(AppYapeContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cuentas = context.Cuentas.Where(o => o.PropietarioId == GetLoggedUser().Id).ToList();

            ViewBag.SaldoTotal = 0;

            foreach(var cuenta in cuentas)
            {
                ViewBag.SaldoTotal += cuenta.Saldo;
            }

            return View(cuentas);
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Cuenta cuenta)
        {
            if (String.IsNullOrEmpty(cuenta.Titulo))
            {
                ModelState.AddModelError("Titulo", "El titulo es obligatorio.");
            }

            if (cuenta.Saldo < 0)
            {
                ModelState.AddModelError("Saldo", "El saldo inicial debe ser mayor a 0.");
            }

            if (ModelState.IsValid)
            {
                cuenta.Propietario = GetLoggedUser();
                context.Cuentas.Add(cuenta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        private Usuario GetLoggedUser()
        {
            var claim = HttpContext.User.Claims.First();
            string username = claim.Value;
            var user = context.Usuarios.First(o => o.Username == username);
            return user;
        }
    }
}

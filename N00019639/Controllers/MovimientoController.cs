using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using N00019639.DB;
using N00019639.Models;
using N00019639.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Controllers
{
    public class MovimientosCuenta {
        public Cuenta Cuenta { get; set; }
        public List<Movimiento> Ingresos { get; set; }
        public List<Movimiento> Gastos { get; set; }
    }

    [Authorize]
    public class MovimientoController : Controller
    {
        private AppYapeContext context;
        private IMovimientoService movimientoService;


        public MovimientoController(AppYapeContext context, IMovimientoService movimientoService)
        {
            this.context = context;
            this.movimientoService = movimientoService;
        }

        public IActionResult VerMovimientos(int cuentaId)
        {
            var cuenta = context.Cuentas.FirstOrDefault(o => o.Id == cuentaId);
            var ingresos = context.Movimientos.Where(o => o.CuentaDestinoId == cuentaId && o.Tipo == TipoMovimiento.Ingreso).ToList();
            var gastos = context.Movimientos.Where(o => o.CuentaDestinoId == cuentaId && o.Tipo == TipoMovimiento.Gasto).ToList();
            return View(new MovimientosCuenta {
                Cuenta = cuenta,
                Ingresos = ingresos,
                Gastos = gastos
            });
        }

        [HttpGet]
        public IActionResult RegistrarIngreso(int cuentaId)
        {
            var cuenta = context.Cuentas.FirstOrDefault(o => o.Id == cuentaId);
            return View(cuenta);
        }

        [HttpGet]
        public IActionResult RegistrarGasto(int cuentaId)
        {
            var cuenta = context.Cuentas.FirstOrDefault(o => o.Id == cuentaId);
            return View(cuenta);
        }

        public IActionResult RegistrarEntreMisCuentas()
        {
            var cuentas = context.Cuentas.Where(o => o.PropietarioId == GetLoggedUser().Id).ToList();
            return View(cuentas);
        }

        public IActionResult RegistrarATerceros(int amigoId)
        {
            var cuentas = context.Cuentas.Where(o => o.PropietarioId == GetLoggedUser().Id).ToList();
            ViewBag.CuentasAmigo = context.Cuentas.Where(o => o.PropietarioId == amigoId).ToList();
            return View(cuentas);
        }

        public IActionResult VerAmigos()
        {
            var usuario = GetLoggedUser();
            var amigos = context.Usuarios.Where(o => o.Id != usuario.Id).ToList();
            return View(amigos);
        }

        [HttpPost]
        public string Registrar(int CuentaOrigenId, int CuentaDestinoId, string Descripcion, int Monto, DateTime Fecha, TipoMovimiento TipoMovimiento)
        {
            if (Monto <= 0)
            {
                throw new Exception("Operacion no valida");
            }

            Movimiento movimiento = null;

            if (TipoMovimiento == TipoMovimiento.Ingreso)
            {
                movimiento = movimientoService.RegistrarIngreso(CuentaDestinoId, Monto, Fecha);
            } else if (TipoMovimiento == TipoMovimiento.Gasto)
            {
                movimiento = movimientoService.RegistrarGasto(CuentaDestinoId, Monto, Fecha);
            } else if (TipoMovimiento == TipoMovimiento.TransferenciaEntreMisCuentas)
            {
                movimientoService.RegistrarTransferenciaCuentasPropias(CuentaOrigenId, CuentaDestinoId, Monto, Fecha);
            } else if (TipoMovimiento == TipoMovimiento.TransferenciaAUnAmigo)
            {
                movimientoService.RegistrarTransferenciaCuentaTerceros(CuentaOrigenId, CuentaDestinoId, Monto);
            }

            if (movimiento != null)
            {
                movimientoService.AgregarDescripcion(movimiento, Descripcion);
            }

            return "Registrado";
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

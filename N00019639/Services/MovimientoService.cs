using N00019639.DB;
using N00019639.Estrategias;
using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Services
{
    public interface IMovimientoService
    {
        public Movimiento RegistrarIngreso(int cuentaId, double monto, DateTime Fecha);
        public Movimiento RegistrarGasto(int cuentaId, double monto, DateTime Fecha);
        public void RegistrarTransferenciaCuentasPropias(int cuentaOrigenId, int cuentaDestinoId, double monto, DateTime Fecha);
        public void RegistrarTransferenciaCuentaTerceros(int cuentaOrigenId, int cuentaDestinoId, double monto);
        public Movimiento AgregarDescripcion(Movimiento movimiento, string descripcion);
    }

    public class MovimientoService : IMovimientoService
    {
        private AppYapeContext context;

        public MovimientoService(AppYapeContext context)
        {
            this.context = context;
        }

        public Movimiento RegistrarIngreso(int cuentaId, double monto, DateTime Fecha)
        {
            var cuenta = context.Cuentas.FirstOrDefault(o => o.Id == cuentaId);
            cuenta.Saldo = cuenta.Saldo + monto;
            context.SaveChanges();
            
            return RegistrarMovimiento(0, cuentaId, monto, TipoMovimiento.Ingreso, Fecha);
        }

        public Movimiento RegistrarGasto(int cuentaId, double monto, DateTime Fecha)
        {
            var cuenta = context.Cuentas.FirstOrDefault(o => o.Id == cuentaId);


            if (monto <= 0 || cuenta.Saldo < monto)
            {
                throw new Exception("Operacion no valida");
            }

            cuenta.Saldo = cuenta.Saldo - monto;
            context.SaveChanges();

            return RegistrarMovimiento(0, cuentaId, monto, TipoMovimiento.Gasto, Fecha);
        }

        public void RegistrarTransferenciaCuentasPropias(int cuentaOrigenId, int cuentaDestinoId, double monto, DateTime Fecha)
        {
            var movimientoOrigen = RegistrarGasto(cuentaOrigenId, monto, Fecha);
            AgregarDescripcion(movimientoOrigen, "Transferencia");
            var movimientoDestino = RegistrarIngreso(cuentaDestinoId, monto, Fecha);
            AgregarDescripcion(movimientoDestino, "Transferencia");
        }

        public void RegistrarTransferenciaCuentaTerceros(int cuentaOrigenId, int cuentaDestinoId, double monto)
        {
            var Fecha = DateTime.Now;
            var movimientoOrigen = RegistrarGasto(cuentaOrigenId, monto, Fecha);
            AgregarDescripcion(movimientoOrigen, "Transferencia a amigo");
            var movimientoDestino = RegistrarIngreso(cuentaDestinoId, monto, Fecha);
            AgregarDescripcion(movimientoDestino, "Transferencia de amigo");
        }

        public Movimiento AgregarDescripcion(Movimiento movimiento, string descripcion)
        {
            var movimientoBd = context.Movimientos.FirstOrDefault(o => o.Id == movimiento.Id);
            movimiento.Descripcion = descripcion;
            context.SaveChanges();
            return movimientoBd;
        }

        private Movimiento RegistrarMovimiento(int cuentaOrigenId, int cuentaDestinoId, double monto, TipoMovimiento tipo, DateTime Fecha)
        {
            var movimiento = new Movimiento
            {
                CuentaOrigenId = cuentaOrigenId,
                CuentaDestinoId = cuentaDestinoId,
                Monto = monto,
                Tipo = tipo,
                Fecha = Fecha
            };
            context.Movimientos.Add(movimiento);
            context.SaveChanges();
            return movimiento;
        }
    }
}

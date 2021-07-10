
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public enum TipoMovimiento
    {
        Ingreso,
        Gasto,
        TransferenciaEntreMisCuentas,
        TransferenciaAUnAmigo
    }

    public class Movimiento
    {
        public int Id { get; set; }
        public double Monto { get; set; }
        public string Descripcion { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public int? CuentaOrigenId { get; set; }
        public Cuenta CuentaOrigen { get; set; }
        public int CuentaDestinoId { get; set; }
        public Cuenta CuentaDestino { get; set; }
        public DateTime Fecha { get; set; }

        public string GetTipo() {
            if (Tipo == TipoMovimiento.Ingreso)
            {
                return "Ingreso";
            }
            else if (Tipo == TipoMovimiento.Gasto)
            {
                return "Gasto";
            }
            else if (Tipo == TipoMovimiento.TransferenciaAUnAmigo)
            {
                return "Transferencia a un amigo";
            }
            else if (Tipo == TipoMovimiento.TransferenciaEntreMisCuentas)
            {
                return "Transferencia entre mis cuentas";
            }
            return "-";
        }
    }
}

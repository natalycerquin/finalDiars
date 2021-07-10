using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int PropietarioId { get; set; }
        public Usuario Propietario { get; set; }
        public string Titulo { get; set; }
        public string Categoria { get; set; }
        public double Saldo { get; set; }
        public List<Movimiento> MovimientosSalida { get; set; }
        public List<Movimiento> MovimientosIngreso { get; set; }
    }
}

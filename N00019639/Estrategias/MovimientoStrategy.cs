using N00019639.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Estrategias
{

    public interface IMovimientoStrategy
    {
        public void Aplicar(Cuenta origen, Cuenta destino, double Monto);
    }

    public class MovimientoStrategy
    {
        private TipoMovimiento tipo;

        public MovimientoStrategy(TipoMovimiento tipo)
        {
            this.tipo = tipo;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Cuenta> Cuentas { get; set; }
        public List<UsuarioAmigo> UsuarioAmigos { get; set; }
    }
}

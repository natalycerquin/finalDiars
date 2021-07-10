using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace N00019639.Models
{
    public class UsuarioAmigo
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int AmigoId { get; set; }
        public Usuario Amigo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Genero
    {
        public int Id { get; set; }
        public string GeneroUsuario { get; set; }
        public virtual IList<Usuario> Usuarios { get; set; }

    }
}
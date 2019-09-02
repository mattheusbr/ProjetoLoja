using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public String Nome { get; set; }
    }
    public class MenuCategoria
    {
        public List<Categoria> Categorias { get; set; }
    }
}
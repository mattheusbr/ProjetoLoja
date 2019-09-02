using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterPc.Types;

namespace MasterPc.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public virtual Usuario Usuario { get; set; }

        public int UsuarioId { get; set; }

        public StatusCompra Status { get; set; }

        public DateTime Data { get; set; }

        public List<CompraProduto> CompraProdutos { get; set; }
    }
}
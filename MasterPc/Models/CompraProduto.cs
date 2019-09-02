using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class CompraProduto
    {
        public int Id { get; set; }

        public Compra Compra { get; set; }

        public int CompraId { get; set; }

        public Produto Produto { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }
    }
}
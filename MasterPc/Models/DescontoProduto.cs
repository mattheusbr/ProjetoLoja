using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class DescontoProduto
    {
        public int Id { get; set; }
        public int QuantidadeDesconto { get; set; }
        public virtual Produto Produto { get; set; }
        public int ProdutoId { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Compra
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        public int Quantidade { get; set; }

        public decimal Valor { get; set; }

        public DateTime CompraHora { get; set; }

    }
}
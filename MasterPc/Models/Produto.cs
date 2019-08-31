using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório."), StringLength(30)]
        public string Nome { get; set; }

        public virtual CategoriaProduto Categoria { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O preço do produto é obrigatório.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatório.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
        public string Descricao { get; set; }

        public string Img { get; set; }

    }
}
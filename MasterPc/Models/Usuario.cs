using MasterPc.Filtros;
using MasterPc.Models;
using MasterPc.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{                                                                                                                  
    public class Usuario
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }
        //public IList<Endereco> Endereco { get; set; }

        //[Required(ErrorMessage = "Genero é obrigatório.")]
        public virtual Genero Genero { get; set; }

        //[Required]
        public int GeneroId { get; set; }

        //public Telefone Telefone;

        [Required(ErrorMessage = "Min 5 - Max 30 Caracteres"), StringLength(maximumLength: 30, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Login é obrigatório.")]
        public string Login { get; set; }

        [Required]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        public int TipoUsuario { get; set; }

      //  public TipoUsuario TipoUsuario { get; set; }

        public IList<Endereco> Enderecos { get; set; }

        public int EnderecoId { get; set; }

    }
}
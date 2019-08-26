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

        [Required(ErrorMessage = "Genero é obrigatório.")]
        public virtual Genero Genero { get; set; }

        public int GeneroId { get; set; }


        [Required(ErrorMessage = "Min 5 - Max 30 Caracteres"), StringLength(maximumLength: 30, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Login é obrigatório.")]
        public string Login { get; set; }

        [Required]
        [CustomValidationCPF(ErrorMessage  = "CPF inválido")]
        public string CPF { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        //===============================================ENDERÇO===============================================\\
        [Required(ErrorMessage = "Endereco obrigatório.")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Numero obrigatório.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Bairro obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Municipio obrigatório.")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "Estado obrigatório.")]
        public string Estado { get; set; }

        //frame que busca CEP no banco do correio
        [Required(ErrorMessage = "CEP obrigatório.")]
        public string cep { get; set; }

        [Required(ErrorMessage = "Complemento obrigatório")]
        public string Complemento { get; set; }



    }
}
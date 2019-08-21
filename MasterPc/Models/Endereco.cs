using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Endereco obrigatório.")]
        public string _Endereco { get; set; }

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
        public string CEP { get; set; }

        [Required(ErrorMessage = "Complemento obrigatório")]
        public string Complemento { get; set; }
    }
}
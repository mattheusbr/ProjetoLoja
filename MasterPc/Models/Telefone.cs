using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterPc.Models
{
    public class Telefone
    {
        public int Id { get; set; }

        [Required]
        public string Celular { get; set; }
        public string TelefoneFixo { get; set; }


    }
}
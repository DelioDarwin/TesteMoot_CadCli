using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TesteMoot.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        public string Logradouro { get; set; }

        public int Numero { get; set; }

        [ValidateNever]
        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }
        public string Pais { get; set; }

        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        [ValidateNever]
        public Cliente Cliente { get; set; }


    }
}


using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteMoot.Models
{
    public class Cliente
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Nome do Cliente")]
        public string Nome { get; set; }

        [DisplayName("Email do Cliente")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        public ICollection<Endereco> Enderecos { get; } = new List<Endereco>();
    }
}

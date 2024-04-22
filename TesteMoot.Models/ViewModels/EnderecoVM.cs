using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMoot.Models.ViewModels
{
    public class EnderecoVM
    {
        public Endereco Endereco { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> listaClientes { get; set; }
    }
}

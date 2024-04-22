using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMoot.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IClienteRepository Cliente { get; }
        IEnderecoRepository Endereco { get; }

        void Save();
    }
}

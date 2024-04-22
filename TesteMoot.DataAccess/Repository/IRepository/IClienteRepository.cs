using TesteMoot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMoot.DataAccess.Repository.IRepository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Update(Cliente obj);
    }
}

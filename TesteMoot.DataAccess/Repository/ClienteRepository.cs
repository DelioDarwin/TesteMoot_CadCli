using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.DataAcess.Data;
using TesteMoot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TesteMoot.DataAccess.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private ApplicationDbContext _db;
        public ClienteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        

        public void Update(Cliente obj)
        {
            _db.Cliente.Update(obj);
        }
    }
}

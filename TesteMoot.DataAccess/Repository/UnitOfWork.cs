using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMoot.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IClienteRepository Cliente { get; private set; }
        public IEnderecoRepository Endereco { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Cliente = new ClienteRepository(_db);
            Endereco = new EnderecoRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

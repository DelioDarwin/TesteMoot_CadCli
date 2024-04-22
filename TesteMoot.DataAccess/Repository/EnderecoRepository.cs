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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        private ApplicationDbContext _db;
        public EnderecoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Endereco obj)
        {
            var objFromDb = _db.Endereco.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Logradouro = obj.Logradouro;
                objFromDb.Numero = obj.Numero;
                objFromDb.Complemento = obj.Complemento;
                objFromDb.Bairro = obj.Bairro;
                objFromDb.Cidade = obj.Cidade;
                objFromDb.Estado = obj.Estado;
                objFromDb.Pais = obj.Pais;
                objFromDb.ClienteId = obj.ClienteId;
            }
        }
    }
}

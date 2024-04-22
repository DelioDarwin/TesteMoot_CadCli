using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.Models;
using TesteMoot.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace TesteMoot.WebApi.Controllers
{
   public class EnderecoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnderecoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Endereco> Index() 
        {
            List<Endereco> objEnderecoList = _unitOfWork.Endereco.GetAll(includeProperties:"Cliente").ToList();
           
            return objEnderecoList;
        }



        [Route(template: "Endereco/Create/")]
        public EnderecoVM Create()
        {
            EnderecoVM enderecoVM = new()
            {
                listaClientes = _unitOfWork.Cliente.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nome,
                    Value = u.Id.ToString()
                }),
                Endereco = new Endereco()
            };

            return enderecoVM;
        }

        [HttpPost]
        [Route(template: "Endereco/Create/")]
        public bool Create([FromBody] EnderecoVM enderecoVM)
        {
            List<Endereco> objEnderecoList;

            if (ModelState.IsValid)
            {
                _unitOfWork.Endereco.Add(enderecoVM.Endereco);
                _unitOfWork.Save();

                return true;
            }
            else
            {
                return false;
            }

         }


        [Route(template: "Endereco/Edit/{id:int}")]
        public  EnderecoVM Edit(int id)
        {
          
            if (id == null || id == 0)
            {
                return null;
            }
                 
            EnderecoVM enderecoVM = new()
            {
                listaClientes = _unitOfWork.Cliente.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Nome,
                    Value = u.Id.ToString()
                }),                
            };

            enderecoVM.Endereco = _unitOfWork.Endereco.Get(u => u.Id == id);

            return enderecoVM;
        }


        [HttpPost]
        [Route(template: "Endereco/Edit/")]
        public EnderecoVM Edit([FromBody] EnderecoVM enderecoVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Endereco.Update(enderecoVM.Endereco);
                _unitOfWork.Save();
            }
            return enderecoVM;

        }

        [HttpGet]
        [Route(template: "Endereco/Delete/{id:int}")]
        public Endereco Delete(int? id)
        {
            Endereco endereco = new Endereco();

            if (id == 0)
            {
                return endereco;
            }

            endereco = _unitOfWork.Endereco.Get(u => u.Id == id);


            return endereco;
        }

        [HttpDelete]
        [Route(template: "Endereco/DeletePOST/{id:int}")]
        public Endereco DeletePOST(int id)
        {
            Endereco endereco = new Endereco();

            if (id == null || id == 0)
            {
                return endereco;
            }

            endereco = _unitOfWork.Endereco.Get(u => u.Id == id);

            if (endereco == null)
            {
                return endereco;
            }

            _unitOfWork.Endereco.Remove(endereco);
            _unitOfWork.Save();

           return endereco;
        }


    }
}

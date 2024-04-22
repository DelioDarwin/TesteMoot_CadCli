using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.DataAcess.Data;
using TesteMoot.Models;
using Microsoft.AspNetCore.Mvc;
using TesteMoot.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace TesteMoot.WebApi.Controllers
{

    public class ClienteController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClienteController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public List<Cliente> Index()
        {
            List<Cliente> objClienteList = _unitOfWork.Cliente.GetAll().ToList();
            return objClienteList;
        }


        [HttpGet]
        [Route(template: "Cliente/Edit/{id:int}")]
        public Cliente Edit(int? id)
        {
            Cliente? clienteyFromDb= new Cliente();

            if (id == null || id == 0)
            {
                return clienteyFromDb;
            }

            clienteyFromDb = _unitOfWork.Cliente.Get(u => u.Id == id);

            if (clienteyFromDb == null)
            {
                return clienteyFromDb;
            }
            return clienteyFromDb;
        }


        [HttpPost]
        [Route(template: "Cliente/Edit/")]
        public IActionResult Edit([FromBody] Cliente obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Cliente.Update(obj);
                _unitOfWork.Save();
            }
            return Ok();

        }

        [HttpGet]
        [Route(template: "Cliente/Delete/{id:int}")]
        public Cliente Delete(int id)
        {
            Cliente? clienteFromDb = new Cliente();
            if (id == null || id == 0)
            {
                return clienteFromDb;
            }
            clienteFromDb = _unitOfWork.Cliente.Get(u => u.Id == id);

            if (clienteFromDb == null)
            {
                return clienteFromDb;
            }
            return clienteFromDb;
        }

        [HttpDelete]
        [Route(template: "Cliente/DeletePOST/{id:int}")]
        public Cliente DeletePOST(int? id)
        {
            Cliente? obj = _unitOfWork.Cliente.Get(u => u.Id == id);
            if (obj == null)
            {
                return obj;
            }

            _unitOfWork.Cliente.Remove(obj);
            _unitOfWork.Save();
            return obj;
        }



        [HttpPost]
        [Route(template: "Cliente/Create/")]
        public Cliente Create([FromBody] Cliente obj)
        {

            if (ModelState.IsValid)
            {
                //if (EmailCadastrado(obj.Email))
                //{
                //    return null;                  
                //}
                //else
                //{
                    _unitOfWork.Cliente.Add(obj);
                    _unitOfWork.Save();
                //}
            }

            return obj;

        }


        [Route(template: "Cliente/EmailLivre/{email}")]
        public async Task<IActionResult> EmailLivre(string email)
        {
            //verifica existencia e-mail
            Cliente? cli = _unitOfWork.Cliente.Get(u => u.Email == email);
            if (cli != null)
            {
                return NotFound(); 
            }
            else
            {
                return Ok();
            }
        }
    }
}

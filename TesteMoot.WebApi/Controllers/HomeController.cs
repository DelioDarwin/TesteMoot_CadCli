using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace TesteMoot.WebApi.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route(template: "Home")]
        public ICollection<Cliente> Index()
        {

            ICollection<Cliente> clienteList = _unitOfWork.Cliente.GetAll(includeProperties: "Cliente,Cliente.Endereco").ToList();

            return clienteList;

        }

        [HttpGet]
        [Route(template: "Home/Details/{clienteId:int}")]
        public ICollection<Endereco> Details(int clienteId)
        {
            ICollection<Endereco> endereco = _unitOfWork.Endereco.GetAllForId(u => u.ClienteId == clienteId).ToList();
            return endereco;
        }


    }
}
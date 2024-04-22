using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace TesteMootWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index()
        //{

        //    ICollection<Cliente> clienteList = _unitOfWork.Cliente.GetAll(includeProperties: "Cliente,Cliente.Endereco").ToList();
        //    return View(clienteList);
        //}

        public IActionResult Index()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            ICollection<Cliente> clientesRetorno;


            HttpResponseMessage response = client.GetAsync("Home").Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                clientesRetorno = JsonConvert.DeserializeObject<List<Cliente>>(sRetorno);
                return View(clientesRetorno.ToList());
            }
            else
            {
                return View();
            }      


        }


        //public IActionResult Details(int clienteId)
        //{
        //    ICollection<Endereco> endereco = _unitOfWork.Endereco.GetAllForId(u => u.ClienteId == clienteId).ToList();
        //    return View(endereco);
        //}


        public IActionResult Details(int clienteId)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            ICollection<Endereco> enderecosRetorno;

            HttpResponseMessage response = client.GetAsync("Home/Details/" + clienteId.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                enderecosRetorno = JsonConvert.DeserializeObject<List<Endereco>>(sRetorno);
                return View(enderecosRetorno.ToList());
            }
            else
            {
                List<Endereco> Retnull = new List<Endereco>();
                return View(Retnull);
            }

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
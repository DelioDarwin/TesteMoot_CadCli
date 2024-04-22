using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.Models;
using TesteMoot.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Newtonsoft.Json;
using System.Text;

namespace TesteMootWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnderecoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EnderecoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index() 
        //{
        //    List<Endereco> objEnderecoList = _unitOfWork.Endereco.GetAll(includeProperties:"Cliente").ToList();
           
        //    return View(objEnderecoList);
        //}

        public IActionResult Index()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            ICollection<Endereco> enderecosRetorno;


            HttpResponseMessage response = client.GetAsync("Endereco").Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                enderecosRetorno = JsonConvert.DeserializeObject<List<Endereco>>(sRetorno);
                return View(enderecosRetorno.ToList());
            }
            else
            {
                return View();
            }

        }


        //public IActionResult Create()
        //{
        //    EnderecoVM enderecoVM = new()
        //    {
        //        listaClientes = _unitOfWork.Cliente.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Nome,
        //            Value = u.Id.ToString()
        //        }),
        //        Endereco = new Endereco()
        //    };

        //    return View(enderecoVM);
        //}

        public IActionResult Create()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            EnderecoVM enderecosVMRetorno;

            HttpResponseMessage response = client.GetAsync("Endereco/Create/").Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                enderecosVMRetorno = JsonConvert.DeserializeObject<EnderecoVM>(sRetorno);
                return View(enderecosVMRetorno);
            }
            else
            {
                EnderecoVM enderecosVMRet = new EnderecoVM();
                return View(enderecosVMRet);
            }
        }


        //[HttpPost]
        //public IActionResult Create(EnderecoVM enderecoVM)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        _unitOfWork.Endereco.Add(enderecoVM.Endereco);

        //        _unitOfWork.Save();
        //        TempData["success"] = "Endereço cadastrado com sucesso!";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        enderecoVM.listaClientes = _unitOfWork.Cliente.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Nome,
        //            Value = u.Id.ToString()
        //        });
        //        return View(enderecoVM);
        //    }

        //}

        [HttpPost]
        public IActionResult Create(EnderecoVM enderecoVM)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");

            var serialized = JsonConvert.SerializeObject(enderecoVM);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            //Passo o objeto via content (pegar no time66)
            HttpResponseMessage response = client.PostAsync("Endereco/Create/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Endereço cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "Ocoreu um erro ao cadastrar, tente novamente informando os dados corretamente";
                return RedirectToAction("Index");
            }
        }



        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            EnderecoVM enderecoVM = new EnderecoVM();
            enderecoVM.Endereco = _unitOfWork.Endereco.Get(u => u.Id == id);

            enderecoVM.listaClientes = _unitOfWork.Cliente.GetAll().Select(u => new SelectListItem
            {
                Text = u.Nome,
                Value = u.Id.ToString()
            });

            return View(enderecoVM);
        }



        //[HttpPost]
        //public IActionResult Edit(EnderecoVM obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Endereco.Update(obj.Endereco);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Endereço atualizado com sucesso!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}

        [HttpPost]
        public IActionResult Edit(EnderecoVM enderecoVM)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            EnderecoVM enderecosVMRetorno;

            var serialized = JsonConvert.SerializeObject(enderecoVM);
            var content = new StringContent(serialized, Encoding.UTF8, "application/json");

            //Passo o objeto via content (pegar no time66)
            HttpResponseMessage response = client.PostAsync("Endereco/Edit/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Endereço atualizado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "Ocoreu um erro ao atulizar, tente novamente informando os dados corretamente";
                return RedirectToAction("Index");
            }
        }



        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Endereco endereco = _unitOfWork.Endereco.Get(u => u.Id == id);


        //    return View(endereco);
        //}


        public IActionResult Delete(int? id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            Endereco enderecoRetorno;

            HttpResponseMessage response = client.GetAsync("Endereco/Delete/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                enderecoRetorno = JsonConvert.DeserializeObject<Endereco>(sRetorno);
                return View(enderecoRetorno);
            }
            else
            {
                Endereco enderecoRet = new Endereco();
                return View(enderecoRet);
            }
        }



        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Endereco endereco = _unitOfWork.Endereco.Get(u => u.Id == id);

        //    if (endereco == null)
        //    {
        //        return NotFound();
        //    }

        //    _unitOfWork.Endereco.Remove(endereco);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Endereço excluído com sucesso!";
        //    return RedirectToAction("Index");
        //}


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            Endereco enderecoRetorno;

            HttpResponseMessage response = client.DeleteAsync("Endereco/DeletePOST/" + id.ToString()).Result;


            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Endereço excluído com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "Ocoreu um erro ao excluir, tente novamente";
                return RedirectToAction("Index");
            }
        }
    }
}

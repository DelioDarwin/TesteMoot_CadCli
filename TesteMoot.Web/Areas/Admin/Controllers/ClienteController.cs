using TesteMoot.DataAccess.Repository.IRepository;
using TesteMoot.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Azure;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Newtonsoft.Json.Converters;

namespace TesteMootWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClienteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ClienteController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        //public IActionResult Index()
        //{
        //    List<Cliente> objClienteList = _unitOfWork.Cliente.GetAll().ToList();
        //    return View(objClienteList);
        //}


        public IActionResult Index()
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            ICollection<Cliente> clientesRetorno;


            HttpResponseMessage response = client.GetAsync("Cliente").Result;

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


        public IActionResult Create()
        {
            return View();
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Cliente? clienteyFromDb = _unitOfWork.Cliente.Get(u => u.Id == id);

        //    if (clienteyFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(clienteyFromDb);
        //}


        public IActionResult Edit(int? id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            Cliente clientesRetorno;

            HttpResponseMessage response = client.GetAsync("Cliente/Edit/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                clientesRetorno = JsonConvert.DeserializeObject<Cliente>(sRetorno);
                return View(clientesRetorno);
            }
            else
            {
                return View();
            }


        }

        //[HttpPost]
        //public IActionResult Edit(Cliente obj, IFormFile? file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (file != null)
        //        {
        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            string productPath = Path.Combine(wwwRootPath, @"images\logos");

        //            if (!string.IsNullOrEmpty(obj.ImageUrl))
        //            {
        //                //delete the old image
        //                var oldImagePath =
        //                    Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

        //                if (System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }
        //            }

        //            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //            obj.ImageUrl = @"\images\logos\" + fileName;
        //        }

        //        _unitOfWork.Cliente.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Cliente atualizado com sucesso!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}


        [HttpPost]
        public IActionResult Edit(Cliente obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\logos");

                    if (!string.IsNullOrEmpty(obj.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\logos\" + fileName;
                }

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7192/");

                var serialized = JsonConvert.SerializeObject(obj);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");


                //Passo o objeto via content (pegar no time66)
                HttpResponseMessage response = client.PostAsync("Cliente/Edit/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Cliente atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
            }
            return View();

        }

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Cliente? categoryFromDb = _unitOfWork.Cliente.Get(u => u.Id == id);

        //    if (categoryFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoryFromDb);
        //}


        public IActionResult Delete(int? id)
        {

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            Cliente clientesRetorno;

            HttpResponseMessage response = client.GetAsync("Cliente/Delete/" + id.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                clientesRetorno = JsonConvert.DeserializeObject<Cliente>(sRetorno);
                return View(clientesRetorno);
            }
            else
            {
                return View();
            }

        }


        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Cliente? obj = _unitOfWork.Cliente.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Cliente.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Cliente excluído com sucesso!";
        //    return RedirectToAction("Index");
        //}



        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            Cliente? obj = _unitOfWork.Cliente.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7192/");
            Cliente clientesRetorno;
            string sEndPoint = "";
            sEndPoint = "Cliente/DeletePOST/" + id.ToString();

            client.BaseAddress = new Uri("https://localhost:7192/");
            HttpResponseMessage response = client.DeleteAsync(sEndPoint).Result;

            if (response.IsSuccessStatusCode)
            {
                string sRetorno = response.Content.ReadAsStringAsync().Result;
                clientesRetorno = JsonConvert.DeserializeObject<Cliente>(sRetorno);

                //Exclui Imagem fisica
                if (!string.IsNullOrEmpty(clientesRetorno.ImageUrl))
                {
                    //delete the old image
                    var oldImagePath =
                        Path.Combine(wwwRootPath, clientesRetorno.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }


                TempData["success"] = "Cliente excluído com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "Ocoreu um erro ao excluir, tente novamente";
                return RedirectToAction("Index");
            }
        }


        


        //[HttpPost]
        //public IActionResult Create(Cliente obj, IFormFile? file)
        //{
        //    //if (obj.Email == obj.Email.ToString())
        //    //{
        //    //    ModelState.AddModelError("email", "The DisplayOrder cannot exactly match the Name.");
        //    //}

        //    if (ModelState.IsValid)
        //    {

        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (file != null)
        //        {
        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //            string productPath = Path.Combine(wwwRootPath, @"images\logos");

        //            if (!string.IsNullOrEmpty(obj.ImageUrl))
        //            {
        //                //delete the old image
        //                var oldImagePath =
        //                    Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

        //                if (System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }
        //            }

        //            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //            obj.ImageUrl = @"\images\logos\" + fileName;
        //        }

        //        _unitOfWork.Cliente.Add(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Cliente cadastrado com sucesso!";
        //        return RedirectToAction("Index");
        //    }
        //    return View();


        //}


        [HttpPost]
        public IActionResult Create(Cliente obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7192/");

                HttpResponseMessage responseEmail = client.GetAsync("Cliente/EmailLivre/" + obj.Email).Result;
                if (!responseEmail.IsSuccessStatusCode)
                {
                    TempData["success"] = "Email já cadastrado!";
                    return RedirectToAction("Create");
                }
                 

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\logos");

                    if (!string.IsNullOrEmpty(obj.ImageUrl)
                        {
                        var oldImagePath =
                            Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.ImageUrl = @"\images\logos\" + fileName;
                }
                else
                {
                    obj.ImageUrl = "";
                }                 

                var serialized = JsonConvert.SerializeObject(obj);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");


                HttpResponseMessage response = client.PostAsync("Cliente/Create/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["success"] = "Cliente cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["warning"] = "Erro ao cadastrar, informe os dados correntamente e tente novamente!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["warning"] = "Erro ao cadastrar, informe os dados correntamente e tente novamente!";
                return RedirectToAction("Index");
            }
        }


    }
}

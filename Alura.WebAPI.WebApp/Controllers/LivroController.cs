using System.Linq;
using Alura.ListaLeitura.Persistencia;
using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Alura.ListaLeitura.HttpClients;

namespace Alura.ListaLeitura.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {

        private readonly IRepository<Livro> _repo;
        private readonly LivroApiClient _livroApiClient;

        public LivroController(IRepository<Livro> repository, LivroApiClient livroApiClient)
        {
            _repo = repository;
            _livroApiClient = livroApiClient;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LivroUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Novo(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                _repo.Incluir(model.ToLivro());
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async  Task<IActionResult> ImagemCapa(int id)
        {
            var img = await _livroApiClient.GetCapaLivrosync(id);

            if (img != null)
            {
                return File(img, "image/png");
            }
            return File("~/images/capas/capa-vazia.png", "image/png");
        }

        public Livro RecuperaLivro(int id)
        {
            return _repo.Find(id);
        }

        [HttpGet] //Task<IActionResult>
        public async Task<IActionResult> Detalhes(int id)
        {
            var model = await _livroApiClient.GetLivroAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            return View(model.ToUpload());
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Detalhes(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                var livro = model.ToLivro();
                if (model.Capa == null)
                {
                    livro.ImagemCapa = _repo.All
                        .Where(l => l.Id == livro.Id)
                        .Select(l => l.ImagemCapa)
                        .FirstOrDefault();
                }
                _repo.Alterar(livro);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remover(int id)
        {
            var model = await _livroApiClient.GetLivroAsync(id);

            if (model == null)
            {
                return NotFound();
            }
            await _livroApiClient.DeleteLivroAsync(model.Id);

            return RedirectToAction("Index", "Home");
        }
        public ActionResult<LivroUpload> DetalhesJson(int id)
        {
            var model = RecuperaLivro(id);
            if (model == null)
            {
                return NotFound();
            }
            return model.ToModel();
        }
    }
}
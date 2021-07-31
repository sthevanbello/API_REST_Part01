using Alura.ListaLeitura.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Alura.ListaLeitura.HttpClients;

namespace Alura.ListaLeitura.WebApp.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {

        
        private readonly LivroApiClient _livroApiClient;

        public LivroController( LivroApiClient livroApiClient)
        {
            _livroApiClient = livroApiClient;
        }

        [HttpGet]
        public IActionResult Novo()
        {
            return View(new LivroUpload());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Novo(LivroUpload model)
        {

            if (ModelState.IsValid)
            {

                await _livroApiClient.PostLivroAsync(model);

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

        //public Livro RecuperaLivro(int id)
        //{
        //    return _repo.Find(id);
        //}

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
        public async Task<IActionResult> Detalhes(LivroUpload model)
        {
            if (ModelState.IsValid)
            {
                await _livroApiClient.PutLivroAsync(model);

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
        //public ActionResult<LivroUpload> DetalhesJson(int id)
        //{
        //    var model = RecuperaLivro(id);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return model.ToModel();
        //}
    }
}
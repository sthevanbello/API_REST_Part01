using Alura.ListaLeitura.Modelos;
using Alura.ListaLeitura.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Alura.ListaLeitura.HttpClients;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Alura.ListaLeitura.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //private readonly IRepository<Livro> _repo;
        private readonly LivroApiClient _livroApiClient;

        public HomeController(LivroApiClient livroApiClient)
        {
            //_repo = repository;
            _livroApiClient = livroApiClient;
        }

        private async Task<IEnumerable<LivroApi>> ListaDoTipo(TipoListaLeitura tipo)
        {
            var lista =  await _livroApiClient.GetListaLeituraAsync(tipo);

            return lista.Livros;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.User.Claims.First(c => c.Type == "Token").Value;

            var model = new HomeViewModel
            {
                ParaLer = await ListaDoTipo(TipoListaLeitura.ParaLer),
                Lendo = await ListaDoTipo(TipoListaLeitura.Lendo),
                Lidos = await ListaDoTipo(TipoListaLeitura.Lidos)
            };
            return View(model);
        }
    }
}
using Livraria.WebApplication.Business;
using Livraria.WebApplication.Helper;
using Livraria.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.WebApplication.Controllers
{
    public class LivroController : Controller
    {
        private const string ACTION_INDEX = "Index";
        private readonly LivrariaApi<LivroViewModel> _apiLivros;
        private readonly GeneroBusiness _generoBusiness;

        public LivroController()
        {
            _apiLivros = new LivrariaApi<LivroViewModel>("api/livraria/livro");
            _generoBusiness = new GeneroBusiness();
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _apiLivros.GetAsync();
            return View(livros.OrderBy(prop => prop.Nome));
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var livro = await _apiLivros.GetAsync(id);
            return View(livro ?? new LivroViewModel());
        }

        public async Task<ActionResult> NovoLivro()
        {
            var generosSelectList = await _generoBusiness.ObterSelectListGeneros();

            var livro = new LivroViewModel
            {
                Generos = generosSelectList
            };

            return View(livro);
        }


        [HttpPost]
        public IActionResult NovoLivro(LivroViewModel livro)
        {
            var res = _apiLivros.PostAsJsonAsync(livro);

            if (res.IsSuccessStatusCode)
                return RedirectToAction(ACTION_INDEX);

            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Editar(int id)
        {
            var livro = await _apiLivros.GetAsync(id);
            livro.Generos = await _generoBusiness.ObterSelectListGeneros();
            return View(livro);
        }

        [HttpPost]
        public IActionResult Editar(int id, LivroViewModel livro)
        {
            var res = _apiLivros.PutAsJsonAsync(id, livro);
            
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(ACTION_INDEX);
            }

            return View();
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await _apiLivros.DeleteAsync(id);
            return RedirectToAction(ACTION_INDEX);
        }

        public IActionResult Sobre()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

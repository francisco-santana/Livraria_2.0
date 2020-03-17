using Livraria.WebApplication.Helper;
using Livraria.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.WebApplication.Controllers
{
    public class GeneroController : Controller
    {
        private const string ACTION_INDEX = "Index";
        private readonly LivrariaApi<GeneroViewModel> _api;

        public GeneroController()
        {
            _api = new LivrariaApi<GeneroViewModel>("api/livraria/genero");
        }

        public async Task<IActionResult> Index()
        {
            var livros = await _api.GetAsync();
            return View(livros.OrderBy(prop => prop.Descricao));
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            var genero = await _api.GetAsync(id);
            return View(genero ?? new GeneroViewModel());
        }

        public ActionResult NovoGenero()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NovoGenero(GeneroViewModel genero)
        {
            var res = _api.PostAsJsonAsync(genero);

            if (res.IsSuccessStatusCode)
                return RedirectToAction(ACTION_INDEX);

            return View();
        }

        [HttpGet]
        public async Task<ViewResult> Editar(int id)
        {
            var genero = await _api.GetAsync(id);
            return View(genero);
        }

        [HttpPost]
        public IActionResult Editar(int id, GeneroViewModel genero)
        {
            var res = _api.PutAsJsonAsync(id, genero);

            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(ACTION_INDEX);
            }

            return View();
        }

        public async Task<IActionResult> Excluir(int id)
        {
            await _api.DeleteAsync(id);
            return RedirectToAction(ACTION_INDEX);
        }
    }
}

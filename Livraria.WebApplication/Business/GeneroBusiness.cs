using Livraria.WebApplication.Helper;
using Livraria.WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.WebApplication.Business
{
    public class GeneroBusiness
    {

        private readonly LivrariaApi<GeneroViewModel> _apiGeneros;

        public GeneroBusiness()
        {
            _apiGeneros = new LivrariaApi<GeneroViewModel>("api/livraria/genero");
        }

        public async Task<SelectList> ObterSelectListGeneros()
        {
            var generos = await _apiGeneros.GetAsync();
            var GenerosSelectList = new SelectList(generos.OrderBy(prop => prop.Descricao), "Id", "Descricao");
            return GenerosSelectList;
        }
    }
}

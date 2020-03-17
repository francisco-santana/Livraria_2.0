using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.Api.Models;

namespace Livraria.Api.Repository
{
    public interface IGeneroRepositorio
    {
        Task AlterarGenero(int id, Genero generoAlterado);
        Task ExcluirGenero(int id);
        Task<Genero> ObterGeneroPorId(int id);
        Task<List<Genero>> ObterGeneros();
        Task SalvarGenero(Genero genero);
    }
}
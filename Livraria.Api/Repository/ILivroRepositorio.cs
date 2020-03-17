using System.Collections.Generic;
using System.Threading.Tasks;
using Livraria.Api.Models;

namespace Livraria.Api.Repository
{
    public interface ILivroRepositorio
    {
        Task AlterarLivro(int id, Livro livroAlterado);
        Task ExcluirLivro(int id);
        Task<Livro> ObterLivroPorId(int id);
        Task<List<Livro>> ObterLivros();
        Task SalvarLivro(Livro livro);
    }
}
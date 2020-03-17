using Livraria.Api.Data;
using Livraria.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Repository
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private readonly LivrariaContext _contexto;

        public LivroRepositorio(LivrariaContext contexto)
        {
            _contexto = contexto;
        }
        
        public Task<List<Livro>> ObterLivros()
        {
            return _contexto.Livros.Include(prop => prop.Genero).ToListAsync();
        }

        public Task<Livro> ObterLivroPorId(int id)
        {
            return _contexto.Livros.Include(prop => prop.Genero)
                                   .FirstOrDefaultAsync(prop => prop.Id == id);
        }

        public async Task SalvarLivro(Livro livro)
        {
            _contexto.Livros.Add(livro);
            await _contexto.SaveChangesAsync();
        }

        public async Task AlterarLivro(int id, Livro livroAlterado)
        {
            var livro = await ObterLivroPorId(id);

            if (livro != null)
            {
                livro.Autor = livroAlterado.Autor;
                livro.Nome = livroAlterado.Nome;
                livro.Quantidade = livroAlterado.Quantidade;
                livro.GeneroId = livroAlterado.GeneroId;

                await _contexto.SaveChangesAsync();
            }
        }

        public async Task ExcluirLivro(int id)
        {
            var livro = await _contexto.Livros.FindAsync(id);

            if (livro != null)
            {
                _contexto.Livros.Remove(livro);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}

using Livraria.Api.Data;
using Livraria.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Repository
{
    public class GeneroRepositorio : IGeneroRepositorio
    {
        private readonly LivrariaContext _contexto;

        public GeneroRepositorio(LivrariaContext contexto)
        {
            _contexto = contexto;
        }
        
        public Task<List<Genero>> ObterGeneros()
        {
            return _contexto.Generos.ToListAsync();
        }

        public Task<Genero> ObterGeneroPorId(int id)
        {
            return _contexto.Generos.FirstOrDefaultAsync(prop => prop.Id == id);
        }

        public async Task SalvarGenero(Genero genero)
        {
            _contexto.Generos.Add(genero);
            await _contexto.SaveChangesAsync();
        }

        public async Task AlterarGenero(int id, Genero generoAlterado)
        {
            var genero = await ObterGeneroPorId(id);

            if (genero != null)
            {
                genero.Descricao = generoAlterado.Descricao;
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task ExcluirGenero(int id)
        {
            var genero = await _contexto.Generos.FindAsync(id);

            if (genero != null)
            {
                _contexto.Generos.Remove(genero);
                await _contexto.SaveChangesAsync();
            }
        }
    }
}

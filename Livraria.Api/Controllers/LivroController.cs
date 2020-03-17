using Livraria.Api.Data;
using Livraria.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers
{
    [Route("api/livraria/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly LivrariaContext _contexto;

        public LivroController(LivrariaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> Get()
        {
            return await _contexto.Livros.Include(prop => prop.Genero).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> Get(int id)
        {
            var livro = await _contexto.Livros.Include(prop => prop.Genero)
                                              .FirstOrDefaultAsync(prop  => prop.Id == id);

            if (livro == null)
                return NotFound();

            return livro;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Livro livro)
        {
            if (!ModelState.IsValid)
                return BadRequest("Informações inválidas");

            _contexto.Livros.Add(livro);
            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Livro livroEditado)
        {
            if (!ModelState.IsValid)
                return BadRequest("Informações inválidas");

            var livro = await _contexto.Livros.FirstOrDefaultAsync(prop => prop.Id == id);

            if (livro == null)
                return NotFound();

            livro.Autor = livroEditado.Autor;
            livro.Nome = livroEditado.Nome;
            livro.Quantidade = livroEditado.Quantidade;
            livro.GeneroId = livroEditado.GeneroId;

            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            var livro = await _contexto.Livros.FindAsync(id);

            if(livro == null)
                return NotFound();

            _contexto.Livros.Remove(livro);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}

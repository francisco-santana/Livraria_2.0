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
    public class GeneroController : ControllerBase
    {
        private readonly LivrariaContext _contexto;

        public GeneroController(LivrariaContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await _contexto.Generos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await _contexto.Generos.FirstOrDefaultAsync(prop  => prop.Id == id);

            if (genero == null)
                return NotFound();

            return genero;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Genero genero)
        {
            if (!ModelState.IsValid)
                return BadRequest("Informações inválidas");

            _contexto.Generos.Add(genero);
            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Genero generoEditado)
        {
            if (!ModelState.IsValid)
                return BadRequest("Informações inválidas");

            var genero = await _contexto.Generos.FirstOrDefaultAsync(prop => prop.Id == id);

            if (genero == null)
                return NotFound();

            genero.Descricao = generoEditado.Descricao;         

            await _contexto.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirGenero(int id)
        {
            var genero = await _contexto.Generos.FindAsync(id);

            if(genero == null)
                return NotFound();

            _contexto.Generos.Remove(genero);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}

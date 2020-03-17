using Livraria.Api.Models;
using Livraria.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers
{
    [Route("api/livraria/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private const string MENSAGEM_INFORMACOES_INVALIDAS = "Informações inválidas";
        private const string PARAMETRO_ID = "{id}";
        private readonly IGeneroRepositorio _repositorio;

        public GeneroController(IGeneroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await _repositorio.ObterGeneros();
        }

        [HttpGet(PARAMETRO_ID)]
        public async Task<ActionResult<Genero>> Get(int id)
        {
            var genero = await _repositorio.ObterGeneroPorId(id);

            if (genero == null)
                return NotFound();

            return genero;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Genero genero)
        {
            if (!ModelState.IsValid)
                return BadRequest(MENSAGEM_INFORMACOES_INVALIDAS);

            await _repositorio.SalvarGenero(genero);

            return Ok();
        }

        [HttpPut(PARAMETRO_ID)]
        public async Task<IActionResult> Editar(int id, Genero generoEditado)
        {
            if (!ModelState.IsValid)
                return BadRequest(MENSAGEM_INFORMACOES_INVALIDAS);

            await _repositorio.AlterarGenero(id, generoEditado);

            return Ok();
        }

        [HttpDelete(PARAMETRO_ID)]
        public async Task<IActionResult> ExcluirGenero(int id)
        {
            await _repositorio.ExcluirGenero(id);
            return NoContent();
        }
    }
}

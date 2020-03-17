using Livraria.Api.Models;
using Livraria.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Livraria.Api.Controllers
{
    [Route("api/livraria/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private const string MENSAGEM_INFORMACOES_INVALIDAS = "Informações inválidas";
        private const string PARAMETRO_ID = "{id}";
        private readonly ILivroRepositorio _repositorio;

        public LivroController(ILivroRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> Get()
        {
            return await _repositorio.ObterLivros();
        }

        [HttpGet(PARAMETRO_ID)]
        public async Task<ActionResult<Livro>> Get(int id)
        {
            var livro = await _repositorio.ObterLivroPorId(id);

            if (livro == null)
                return NotFound();

            return livro;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Livro livro)
        {
            if (!ModelState.IsValid)
                return BadRequest(MENSAGEM_INFORMACOES_INVALIDAS);

            await _repositorio.SalvarLivro(livro);

            return Ok();
        }

        [HttpPut(PARAMETRO_ID)]
        public async Task<IActionResult> Editar(int id, Livro livroEditado)
        {
            if (!ModelState.IsValid)
                return BadRequest(MENSAGEM_INFORMACOES_INVALIDAS);

            await _repositorio.AlterarLivro(id, livroEditado);

            return Ok();
        }

        [HttpDelete(PARAMETRO_ID)]
        public async Task<IActionResult> ExcluirLivro(int id)
        {
            await _repositorio.ExcluirLivro(id);
            return NoContent();
        }
    }
}

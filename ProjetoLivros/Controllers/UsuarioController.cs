using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLivros.Interfaces;
using ProjetoLivros.Models;
using ProjetoLivros.Validators;

namespace ProjetoLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var usuarios = _repository.ListarTodos();

            return Ok(usuarios);
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            var validacao = new UsuarioValidator().Validate(usuario);

            if(!validacao.IsValid)
            {
                var erros = validacao.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(erros);
            }

            _repository.Cadastrar(usuario);

            return Created();
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var usuario = _repository.ListarPorId(id);

            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Usuario usuarioNovo)
        {
            var usuarioAtualizado = _repository.Atualizar(id, usuarioNovo);

            if (usuarioAtualizado == null) return NotFound();

            return Ok(usuarioAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var usuarioDeletado = _repository.Deletar(id);

            if (usuarioDeletado == null) return NotFound();

            return Ok(usuarioDeletado);
        }
    }
}

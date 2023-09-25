
using Blog_Pessoal.Model;
using Blog_Pessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Pessoal.Controllers
{
    [Route("~/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;

        public PostagemController(IPostagemService postagemService,
            IValidator<Postagem> postagemValidator)
        {
            _postagemService = postagemService;
            _postagemValidator = postagemValidator;
        }

        [HttpGet] // get para trazer informação
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }

        [HttpGet("{id}")] // variável de caminho "{id}"
        public async Task<ActionResult> GetById(long id)
        {
            var Resposta = await _postagemService.GetById(id);

            if (Resposta is null)
                return NotFound();

            return Ok(Resposta);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo(string titulo)
        {
            return Ok(await _postagemService.GetByTitulo(titulo));

        }

        [HttpPost] // post para enviar informação
        public async Task<ActionResult> Create([FromBody] Postagem postagem)
        {
            var validarPostagem = await _postagemValidator.ValidateAsync(postagem); //usando o postagem validator, valida o objeto postagem

            if (!validarPostagem.IsValid) //exclamação no começo da operação = sinal de negação
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);

            await _postagemService.Create(postagem);

            return CreatedAtAction(nameof(GetById), new {id = postagem.Id}, postagem);
        
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Postagem postagem)
        {
            if (postagem.Id == 0)
                return BadRequest("Id da Postagem é inválido");

            var validarPostagem = await _postagemValidator.ValidateAsync(postagem); //usando o postagem validator, valida o objeto postagem

            if (!validarPostagem.IsValid) //exclamação no começo da operação = sinal de negação
                return StatusCode(StatusCodes.Status400BadRequest, validarPostagem);
            
            var Resposta = await _postagemService.Update(postagem); // atualização precisa ser guardada em uma variável

            if (Resposta is null)
                return NotFound("Postagem não encontrada!");

            return Ok(Resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var BuscaPostagem = await _postagemService.GetById(id); //checar se a postagem existe

            if (BuscaPostagem is null)
                return NotFound("Postagem não encontrada!");

            await _postagemService.Delete(BuscaPostagem); //recebe o objeto encontrado e armazenado na variavel para deletar

            return NoContent();
        }

    }
}

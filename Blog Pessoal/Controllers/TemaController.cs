using Blog_Pessoal.Model;
using Blog_Pessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Blog_Pessoal.Controllers
{
    [Route("~/temas")]
    [ApiController]
    public class TemaController : ControllerBase
    {              
            private readonly ITemaService _TemaService;
            private readonly IValidator<Tema> _TemaValidator;

            public TemaController(ITemaService TemaService,
                IValidator<Tema> TemaValidator)
            {
                _TemaService = TemaService;
                _TemaValidator = TemaValidator;
            }

            [HttpGet] // get para trazer informação
            public async Task<ActionResult> GetAll()
            {
                return Ok(await _TemaService.GetAll());
            }

            [HttpGet("{id}")] // variável de caminho "{id}"
            public async Task<ActionResult> GetById(long id)
            {
                var Resposta = await _TemaService.GetById(id);

                if (Resposta is null)
                    return NotFound();

                return Ok(Resposta);
            }

        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult> GetByDescricao(string descricao)
        {
            return Ok(await _TemaService.GetByDescricao(descricao));

        }

        [HttpPost] // post para enviar informação
            public async Task<ActionResult> Create([FromBody] Tema Tema)
            {
                var validarTema = await _TemaValidator.ValidateAsync(Tema); //usando o Tema validator, valida o objeto Tema

                if (!validarTema.IsValid) //exclamação no começo da operação = sinal de negação
                    return StatusCode(StatusCodes.Status400BadRequest, validarTema);

                await _TemaService.Create(Tema);

                return CreatedAtAction(nameof(GetById), new { id = Tema.Id }, Tema);

            }

            [HttpPut]
            public async Task<ActionResult> Update([FromBody] Tema Tema)
            {
                if (Tema.Id == 0)
                    return BadRequest("Id do Tema é inválido");

                var validarTema = await _TemaValidator.ValidateAsync(Tema); //usando o Tema validator, valida o objeto Tema

                if (!validarTema.IsValid) //exclamação no começo da operação = sinal de negação
                    return StatusCode(StatusCodes.Status400BadRequest, validarTema);

                var Resposta = await _TemaService.Update(Tema); // atualização precisa ser guardada em uma variável

                if (Resposta is null)
                    return NotFound("Tema não encontrado!");

                return Ok(Resposta);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(long id)
            {
                var BuscaTema = await _TemaService.GetById(id); //checar se a Tema existe

                if (BuscaTema is null)
                    return NotFound("Tema não encontrado!");

                await _TemaService.Delete(BuscaTema); //recebe o objeto encontrado e armazenado na variavel para deletar

                return NoContent();
            }

        }
    }
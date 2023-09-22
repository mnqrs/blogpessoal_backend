
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

        [HttpGet]
        public async Task<ActionResult> GettAll()
        {
            return Ok(await _postagemService.GetAll());
        }

       

        
    }
}

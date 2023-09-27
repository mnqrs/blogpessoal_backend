using Blog_Pessoal.Model;
using FluentValidation;

namespace Blog_Pessoal.Validator
{
    public class TemaValidator : AbstractValidator<Tema>
    {

        public TemaValidator()
        {
            RuleFor(t => t.Descricao)
                .NotEmpty();
        }

    }
}
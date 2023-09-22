using Blog_Pessoal.Model;
using FluentValidation;

namespace Blog_Pessoal.Validator
{
    public class PostagemValidator : AbstractValidator<Postagem>
    {
        public PostagemValidator() {

            RuleFor(p => p.Titulo)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(100);

            RuleFor(p => p.Texto)
                    .NotEmpty()
                    .MinimumLength(10)
                    .MaximumLength(1000);

        }

    }
}

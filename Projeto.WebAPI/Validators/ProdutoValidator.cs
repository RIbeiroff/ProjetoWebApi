using FluentValidation;
using Microsoft.AspNetCore.Http;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator(){
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("Nome inválido");
            RuleFor(p => p.Qtde_Estoque)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantidade do produto inválido");            
            RuleFor(p => p.Valor_Unitario)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Valor do produto inválido");
        }
    }
}
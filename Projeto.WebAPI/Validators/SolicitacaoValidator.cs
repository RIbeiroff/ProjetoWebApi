using FluentValidation;
using Projeto.WebAPI.Model;

namespace Projeto.WebAPI.Validators
{
    public class SolicitacaoValidator : AbstractValidator<Solicitacao>
    {
        public SolicitacaoValidator()
        {
            RuleFor(c => c.Produto_Id)
                .NotEmpty()
                .WithMessage("Id do produto inválido")
                .GreaterThan(0)
                .WithMessage("Id do produto deve ser maior que zero");
            RuleFor(c => c.Qtde_Comprada)
                .NotEmpty()
                .WithMessage("Quantidade de produtos inválido")
                .GreaterThan(0)
                .WithMessage("A quantidade de produtos da compra deve ser maior que zero");
            RuleFor(c => c.Cartao.Titular)
                .NotEmpty()
                .WithMessage("Nome do titular do cartão é inválido");
            RuleFor(c => c.Cartao.Numero)
                .CreditCard()
                .WithMessage("Número de cartão de crédito inválido");
            RuleFor(c => c.Cartao.Bandeira)
                .NotEmpty()
                .WithMessage("Bandeira do cartão inválida");
            RuleFor(c => c.Cartao.Cvv)
                .Length(3)
                .WithMessage("Cvc inválido");
            // Falta validar a data de expiração
        }
    }
}
using Base.Application.Features.Orders.Commands;
using FluentValidation;

namespace Base.Application.Features.Orders.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Pedido precisa ter pelo menos um item");

            RuleForEach(x => x.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).NotEmpty().WithMessage("Produto inválido");
                item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantidade deve ser maior que 0");
            });
        }
    }
}

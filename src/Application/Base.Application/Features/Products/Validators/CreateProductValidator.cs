using Base.Application.Features.Auth.Commands;
using Base.Application.Features.Products.Commands;
using FluentValidation;

namespace Base.Application.Features.Products.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(200).WithMessage("O título deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
            .LessThanOrEqualTo(999999.99m).WithMessage("O preço é muito alto.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("A descrição deve ter no máximo 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("A categoria é obrigatória.")
            .MaximumLength(100).WithMessage("A categoria deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("A imagem é obrigatória.")
            .MaximumLength(500).WithMessage("A URL da imagem deve ter no máximo 500 caracteres.");

        RuleFor(x => x.Rating)
            .NotNull().WithMessage("Rating é obrigatório.");

        RuleFor(x => x.Rating.Rate)
            .InclusiveBetween(0, 5).WithMessage("A nota deve estar entre 0 e 5.");

        RuleFor(x => x.Rating.Count)
            .GreaterThanOrEqualTo(0).WithMessage("A quantidade de avaliações deve ser positiva.");
    }
}

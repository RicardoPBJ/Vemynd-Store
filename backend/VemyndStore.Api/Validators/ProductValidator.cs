using FluentValidation;
using VemyndStore.Api.Data.Models;

namespace VemyndStore.Api.Validators
{
    /// <summary>
    /// Validador de produto utilizado para garantir integridade dos dados antes de persistir ou expor via API.
    /// Regras ajustadas conforme necessidades de negócio.
    /// </summary>
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            // Garante que o nome do produto seja informado (não pode ser vazio ou nulo)
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("O campo 'Name' é obrigatório.");

            // Garante que o preço do produto seja maior que zero
            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("O campo 'Price' deve ser maior que zero.");

            // Garante que a data de lançamento seja após 01/01/2000
            RuleFor(p => p.ReleaseDate)
                .GreaterThan(new DateTime(2000, 1, 1))
                .WithMessage("A data de lançamento deve ser após 01/01/2000.");

            // Garante que o peso do produto seja maior que zero
            RuleFor(p => p.Weight)
                .GreaterThan(0).WithMessage("O campo 'Weight' deve ser maior que zero.");

            // Se o produto for touchscreen, a resolução da tela deve ser informada
            RuleFor(p => p.DisplayResolution)
                .NotEmpty()
                .When(p => p.IsTouchscreen)
                .WithMessage("Se o produto for touchscreen, o campo 'DisplayResolution' deve ser informado.");

            // Se o preço for acima de 10.000, a marca deve ser informada
            RuleFor(p => p.Brand)
                .NotEmpty()
                .When(p => p.Price > 10000)
                .WithMessage("Produtos com preço acima de 10.000 devem ter a marca informada.");

            // Se a URL da imagem for informada, ela deve ser uma URL válida
            RuleFor(p => p.ImageUrl)
                .Must(url => string.IsNullOrWhiteSpace(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("Se informado, o campo 'ImageUrl' deve conter uma URL válida.");
        }
    }
}
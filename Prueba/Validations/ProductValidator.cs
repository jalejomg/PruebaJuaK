using FluentValidation;
using Prueba.Modelos;

namespace Prueba.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El parametro Name no puede estar vacio")
                .MaximumLength(20).WithMessage("El parametro Name puede tener maximo 20 caracteres");

            RuleFor(p => p.Price)
                .NotNull().WithMessage("El parametro Price no puede ser nulo")
                .Must(price => price > 0).WithMessage("El precio debe ser mayor a cero");
        }
    }
}

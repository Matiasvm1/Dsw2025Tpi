using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos.Products;
using FluentValidation;
namespace Dsw2025Tpi.Application.Validators.Product
{
    public class UpdateProductValidator: AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator() {
           
            RuleFor(p => p.Id)
           .NotEmpty().WithMessage("El ID es obligatorio.");

            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("El SKU es obligatorio.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(p => p.CurrentUnitPrice)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo.");
        }
    }
}

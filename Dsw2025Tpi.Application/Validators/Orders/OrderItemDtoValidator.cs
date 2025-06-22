using Dsw2025Tpi.Application.Dtos.Orders;
using FluentValidation;

namespace Dsw2025Tpi.Application.Validators.Orders;

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator() 
    {
        RuleFor(i => i.ProductId)
            .NotEmpty().WithMessage("El producto es obligatorio");
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio");
        RuleFor(i => i.UnitPrice)
            .GreaterThan(0).WithMessage("El precio unitario debe ser mayor que cero");
        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero");
    }
}

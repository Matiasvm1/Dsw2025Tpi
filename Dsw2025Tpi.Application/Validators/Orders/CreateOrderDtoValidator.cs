using Dsw2025Tpi.Application.DTOs.Orders;
using FluentValidation;

namespace Dsw2025Tpi.Application.Validators.Orders;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(o => o.CustomerId)
            .NotEmpty().WithMessage("El ID del cliente es obligatorio");
        RuleFor(o => o.ShippingAddress)
            .NotEmpty().WithMessage("La dirección de envío es obligatoria");
        RuleFor(o => o.BillingAddress)
            .NotEmpty().WithMessage("La dirección de facturación es obligatoria");
        RuleFor(o => o.OrderItems)
            .NotNull().WithMessage("La orden debe tener Items")
            .Must(items => items.Any()).WithMessage("La orden debe tener al menos un item");
        RuleForEach(o => o.OrderItems)
            .SetValidator(new OrderItemDtoValidator());
    }
}

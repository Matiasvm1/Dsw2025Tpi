using Dsw2025Tpi.Application.Dtos.Orders;
using FluentValidation;

namespace Dsw2025Tpi.Application.Validators.Orders;

public class UpdateOrderStatusDtoValidator : AbstractValidator<UpdateOrderStatusDto>
{
    public UpdateOrderStatusDtoValidator()
    {
        RuleFor(o => o.NewStatus)
            .IsInEnum().WithMessage("El estado de la orden no es válido");
    }
}

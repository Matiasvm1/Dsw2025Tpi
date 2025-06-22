using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Application.Exceptions;

public class InvalidOrderStatusException : ApplicationException
{
    public InvalidOrderStatusException(OrderStatus actual, OrderStatus nuevo)
        : base($"No se permite camnbiar el estado de ´{actual}´ a ´{nuevo}´") {}
}

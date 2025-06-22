namespace Dsw2025Tpi.Application.Exceptions;

public class OrderNotFoundException : ApplicationException
{
    public OrderNotFoundException(Guid id)
        : base($"No se encontro la orden con ID ´{id}´") {}
}

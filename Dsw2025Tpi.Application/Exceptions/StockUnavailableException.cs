namespace Dsw2025Tpi.Application.Exceptions;

public class StockUnavailableException : ApplicationException
{
    public StockUnavailableException(string productName)
        : base($"No hay stock suficiente para el produto ´{productName}´") {}
}

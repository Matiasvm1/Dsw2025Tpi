namespace Dsw2025Tpi.Application.Dtos.Orders;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}

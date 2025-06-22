using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Dtos.Orders;

public class OrderItemDto
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public static OrderItemDto FromEntity(OrderItem item)
    {
        return new OrderItemDto
        {
            ProductId = item.ProductId,
            Name = item.Name,
            Description = item.Description,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity
        };
    }
}

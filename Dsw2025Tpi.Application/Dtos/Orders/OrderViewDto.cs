using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Application.Dtos.Orders;

public class OrderViewDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string ShippingAddress { get; set; } = null!;
    public string BillingAddress { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> Items { get; set; } = new ();
}

namespace Dsw2025Tpi.Application.Dtos.Orders;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public string ShippingAddress { get; set; } = null!;
    public string BillingAddress { get; set; } = null!;
    public List<OrderItemDto> OrderItems { get; set; } = new ();
}

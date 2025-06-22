using Dsw2025Tpi.Domain.Entities;
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

    public static OrderViewDto FromEntity(Order order)
    {
        return new OrderViewDto
        {
            Id = order.Id,
            CustomerId = order.CustomerId,
            ShippingAddress = order.ShippingAddress,
            BillingAddress = order.BillingAddress,
            CreatedAt = order.CreatedAt,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            Items = order.Items.Select(OrderItemDto.FromEntity).ToList()
        };
    }
}

using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Application.Dtos.Orders;

public class OrderSummaryDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
}

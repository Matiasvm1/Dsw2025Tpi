using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Application.Dtos.Orders;

public class UpdateOrderStatusDto
{
    public OrderStatus NewStatus { get; set; }
}

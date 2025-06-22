
using Dsw2025Tpi.Application.Dtos.Orders;
using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Application.Interfaces;

public interface IOrderService
{
    Task<OrderViewDto> CreateAsync(CreateOrderDto dto);
    Task<OrderViewDto> GetByIdAsync(Guid id);
    Task<IEnumerable<OrderSummaryDto>> GetAllAsync(OrderStatus? status = null, Guid? customerId = null, int pageNumber = 1, int pageSize = 10);
    Task<OrderViewDto?> UpdateStatusAsync(Guid id, UpdateOrderStatusDto dto);
}

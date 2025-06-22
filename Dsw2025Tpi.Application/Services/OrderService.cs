using Dsw2025Tpi.Application.Dtos.Orders;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Enums;
using Dsw2025Tpi.Domain.Interfaces;
using System.Net.NetworkInformation;

namespace Dsw2025Tpi.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository _repository;

    public OrderService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderViewDto> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order(dto.CustomerId, dto.ShippingAddress, dto.BillingAddress);

        foreach(var itemDto in dto.OrderItems)
        {
            var product = await _repository.GetById<Product>(itemDto.ProductId);

            if (product is null || !product.IsActive)
                throw new StockUnavailableException(itemDto.Name);
            if (itemDto.Quantity > product.StockQuantity)
                throw new StockUnavailableException(itemDto.Name);

            var orderItem = new OrderItem(
                productId: product.Id,
                name: product.Name,
                description: product.Description,
                unitPrice: product.CurrentUnitPrice,
                quantity: itemDto.Quantity
            );

            order.AddItem(orderItem);

            product.DecreaseStock(itemDto.Quantity);
            await _repository.Update(product);
        }

        await _repository.Add(order);
        return MapToOrderViewDto(order);
    }

    public async Task<OrderViewDto?> GetByIdAsync(Guid id)
    {
        var order = await _repository.GetById<Order>(id, "items");
        return order is null ? null : MapToOrderViewDto(order);
    }

    public async Task<IEnumerable<OrderSummaryDto>> GetAllAsync(OrderStatus? status, Guid? customerId, int pageNumber, int pageSize)
    {
        var orders = await _repository.GetFiltered<Order>(
            o => (!status.HasValue || o.Status == status.Value) && 
                 (!customerId.HasValue || o.CustomerId == customerId),
            "items"
        );

        return orders?
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OrderSummaryDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalAmount
            }) ?? Enumerable.Empty<OrderSummaryDto>();
    }

    public async Task<OrderViewDto?> UpdateStatusAsync(Guid id, UpdateOrderStatusDto dto)
    {
        var order = await _repository.GetById<Order>(id);

        if (order is null)
            throw new OrderNotFoundException(id);

        if (!IsValidStatusTransition(order.Status, dto.NewStatus))
            throw new InvalidOrderStatusException(order.Status, dto.NewStatus);

        order.ChangeSatus(dto.NewStatus);
        await _repository.Update(order);
        return MapToOrderViewDto(order);
    }

    private static bool IsValidStatusTransition(OrderStatus actual, OrderStatus nuevo)
    {
        if (actual == OrderStatus.Cancelled || actual == OrderStatus.Delivered)
            return false;
        if (actual == OrderStatus.Pending && nuevo == OrderStatus.Processing)
            return true;
        if (actual == OrderStatus.Processing && nuevo == OrderStatus.Shipped)
            return true;
        if (actual == OrderStatus.Shipped && nuevo == OrderStatus.Delivered)
            return true;
        return false;
    }

    private static OrderViewDto MapToOrderViewDto(Order order)
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
            Items = order.Items.Select(i => new OrderItemDto
            {
                ProductId = i.ProductId,
                Name = i.Name,
                Description = i.Description,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}

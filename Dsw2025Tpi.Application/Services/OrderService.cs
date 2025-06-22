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
        await ValidateStockForOrderItems(dto.OrderItems);
        var items = MapItemsFromDto(dto.OrderItems);

        var order = new Order(dto.CustomerId, dto.ShippingAddress, dto.BillingAddress);

        foreach (var item in items)
            order.AddItem(item);

        await UpdateStockForProducts(dto.OrderItems);

        await _repository.Add(order);
        return OrderViewDto.FromEntity(order);
    }

    public async Task<OrderViewDto> GetByIdAsync(Guid id)
    {
        var order = await GetOrderOrThrow(id);
        return OrderViewDto.FromEntity(order);
    }

    public async Task<IEnumerable<OrderSummaryDto>> GetAllAsync(OrderStatus? status, Guid? customerId, int pageNumber, int pageSize)
    {
        var orders = await _repository.GetFiltered<Order>(o =>
            (status == null || o.Status == status) &&
            (customerId == null || o.CustomerId == customerId), "Items");

        return orders?.Select(OrderSummaryDto.FromEntity) ?? Enumerable.Empty<OrderSummaryDto>();
    }

    public async Task<OrderViewDto?> UpdateStatusAsync(Guid id, UpdateOrderStatusDto dto)
    {
        var order = await GetOrderOrThrow(id);
        ValidateStatusTransition(order.Status, dto.NewStatus);
        order.ChangeStatus(dto.NewStatus);

        await _repository.Update(order);
        return OrderViewDto.FromEntity(order);
    }

    // Métodos auxiliares internos
    private async Task<Order> GetOrderOrThrow(Guid id)
    {
        var order = await _repository.GetById<Order>(id, "Items");
        if (order is null)
            throw new OrderNotFoundException(id);
        return order;
    }

    private async Task ValidateStockForOrderItems(IEnumerable<OrderItemDto> items)
    {
        foreach (var item in items)
        {
            var product = await _repository.GetById<Product>(item.ProductId);
            if (product is null || !product.IsActive || item.Quantity > product.StockQuantity)
                throw new StockUnavailableException(item.Name);
        }
    }

    private async Task UpdateStockForProducts(IEnumerable<OrderItemDto> items)
    {
        foreach (var item in items)
        {
            var product = await _repository.GetById<Product>(item.ProductId);
            if (product is not null)
            {
                product.DecreaseStock(item.Quantity);
                await _repository.Update(product);
            }
        }
    }

    private List<OrderItem> MapItemsFromDto(IEnumerable<OrderItemDto> dtos)
    {
        return dtos.Select(dto =>
            new OrderItem(dto.ProductId, dto.Name, dto.Description, dto.UnitPrice, dto.Quantity)).ToList();
    }

    private void ValidateStatusTransition(OrderStatus current, OrderStatus next)
    {
        if (current == OrderStatus.Cancelled || current == OrderStatus.Delivered)
            throw new InvalidOrderStatusException(current, next);
    }
}

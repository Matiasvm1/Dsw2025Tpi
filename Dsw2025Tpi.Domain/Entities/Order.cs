using Dsw2025Tpi.Domain.Enums;

namespace Dsw2025Tpi.Domain.Entities;

public class Order : EntityBase
{
    public Guid CustomerId { get; private set; }
    public string ShippingAddress { get; private set; } = null!;
    public string BillingAddress { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() {}

    public Order(Guid customerId, string shippingAddress, string billingAddress)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("El Cliente es Obligatorio", nameof(customerId));
        if (string.IsNullOrWhiteSpace(shippingAddress))
            throw new ArgumentException("La direccion de envio es obligatoria", nameof(shippingAddress));

        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;
    }

    public void AddItem(OrderItem item)
    {
        _items.Add(item);
    }

    public decimal TotalAmount => _items.Sum(i => i.SubTotal);

    public void ChangeStatus(OrderStatus newStatus)
    {
        Status = newStatus;
    }
}

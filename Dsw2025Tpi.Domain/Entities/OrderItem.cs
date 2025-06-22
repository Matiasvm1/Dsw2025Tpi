namespace Dsw2025Tpi.Domain.Entities;

public class OrderItem : EntityBase
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public OrderItem() { }

    public OrderItem(Guid productId, string name, string description, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre del producto es obligatorio");
        if (unitPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice), "El precio unitario debe ser mayor a cero");
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad debe ser mayor a cero");

        ProductId = productId;
        Name = name;
        Description = description;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }

    public decimal SubTotal => UnitPrice * Quantity;
}

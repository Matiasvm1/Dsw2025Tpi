using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Domain.Entities
{
    public class Product : EntityBase
    {
       
        public string Sku { get; private set; } = null!;
        public string? InternalCode { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal CurrentUnitPrice { get; private set; }
        public int StockQuantity { get; private set; }
        public bool IsActive { get; private set; }

        private Product()
        {

        }
        public Product(string sku, string? internalCode, string name, string? description, decimal currentUnitPrice, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(sku))
                throw new ArgumentException("SKU es obligatorio.", nameof(sku));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del producto es obligatorio.", nameof(name));

            if (currentUnitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(currentUnitPrice), "El precio debe ser mayor a cero.");

            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(stockQuantity), "El stock no puede ser negativo.");


            Sku = sku;
            InternalCode = internalCode;
            Name = name;
            Description = description;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
            IsActive = true;
        }
        public void Update(string name, string? description, decimal unitPrice, int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio.", nameof(name));
            if (unitPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "El precio debe ser mayor a cero.");
            if (stockQuantity < 0)
                throw new ArgumentOutOfRangeException(nameof(stockQuantity), "El stock no puede ser negativo.");

            Name = name;
            Description = description;
            CurrentUnitPrice = unitPrice;
            StockQuantity = stockQuantity;
        }

        public void Disable()
        {
            IsActive = false;
        }

        public void DecreaseStock(int quantity)
        {
            if (!IsActive)
                throw new InvalidOperationException("No se puede modificar el stock de un producto inactivo.");

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), "La cantidad debe ser mayor que cero.");

            if (quantity > StockQuantity)
                throw new InvalidOperationException("No hay suficiente stock disponible.");

            StockQuantity -= quantity;
        }
    }
}

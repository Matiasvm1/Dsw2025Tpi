using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Domain.Entities;

namespace Dsw2025Tpi.Application.Dtos.Products;

public class ProductViewDto
{
    public Guid Id { get; set; }
    public string Sku { get; set; } = null!;
    public string? InternalCode { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal CurrentUnitPrice { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }

    public static ProductViewDto FromEntity(Product product)
    {
        return new ProductViewDto
        {
            Id = product.Id,
            Sku = product.Sku,
            InternalCode = product.InternalCode,
            Name = product.Name,
            Description = product.Description,
            CurrentUnitPrice = product.CurrentUnitPrice,
            StockQuantity = product.StockQuantity,
            IsActive = product.IsActive
        };
    }
}

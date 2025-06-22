using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos.Product
{
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
    }
}

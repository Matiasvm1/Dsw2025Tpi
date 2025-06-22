using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos.Product
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? InternalCode { get; set; }
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}

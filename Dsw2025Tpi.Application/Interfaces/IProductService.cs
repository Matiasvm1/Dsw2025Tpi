using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos.Products;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewDto>> GetAllProductAsync();
        Task<ProductViewDto?> GetProductByIdAsync(Guid id);
        Task<ProductViewDto> CreateProductAsync(CreateProductDto dto);
        Task<ProductViewDto?> UpdateProductAsync(UpdateProductDto dto);
        Task DisableProductAsync(Guid id);
    }
}

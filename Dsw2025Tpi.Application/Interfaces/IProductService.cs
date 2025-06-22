using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos.Product;

namespace Dsw2025Tpi.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewDto>> GetAllAsync();
        Task<ProductViewDto?> GetByIdAsync(Guid id);
        Task<ProductViewDto> CreateAsync(CreateProductDto dto);
        Task<ProductViewDto?> UpdateAsync(UpdateProductDto dto);
        Task DisableAsync(Guid id);
    }
}

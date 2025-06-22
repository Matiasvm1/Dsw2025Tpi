using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025Tpi.Application.Dtos.Products;
using Dsw2025Tpi.Application.Exceptions;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Interfaces;

namespace Dsw2025Tpi.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository _repository;


    public ProductService(IRepository repository)
    {
        _repository = repository;
    }


    public async Task<ProductViewDto> CreateProductAsync(CreateProductDto dto)
    {
        await EnsureProductSkuIsUnique(dto.Sku);

        var product = dto.ToEntity();
        await _repository.Add(product);

        return ProductViewDto.FromEntity(product);
    }

    public async Task DisableProductAsync(Guid id)
    {
        var product = await GetActiveProductOrThrow(id);

        product.Disable();
        await _repository.Update(product);
    }

    public async Task<IEnumerable<ProductViewDto>> GetAllProductAsync()
    {
        var products = await _repository.GetFiltered<Product>(p => p.IsActive)
             ?? Enumerable.Empty<Product>();

        return products.Select(ProductViewDto.FromEntity);
       
    }

    public async Task<ProductViewDto?> GetProductByIdAsync(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        return (product is null || !product.IsActive) ? null : ProductViewDto.FromEntity(product);
    }

    public async Task<ProductViewDto?> UpdateProductAsync(UpdateProductDto dto)
    {
        var product = await GetActiveProductOrThrow(dto.Id);

        product.Update(dto.Name, dto.Description, dto.CurrentUnitPrice, dto.StockQuantity);
        await _repository.Update(product);

        return ProductViewDto.FromEntity(product);
    }

    //METODOS AUXILIARES
    private async Task<Product> GetActiveProductOrThrow(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        if (product is null || !product.IsActive)
            throw new ProductNotFoundException(id);

        return product;
    }
    private async Task EnsureProductSkuIsUnique(string sku)
    {
        var exists = await _repository.First<Product>(p => p.Sku == sku);
        if (exists is not null)
            throw new SkuAlreadyExistsException(sku);
    }
}

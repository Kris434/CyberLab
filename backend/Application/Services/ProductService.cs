using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;

public class ProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> SearchAsync(string search)
    {
        var products = await _repository.SearchAsync(search);

        return products
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price))
            .ToList();
    }
}
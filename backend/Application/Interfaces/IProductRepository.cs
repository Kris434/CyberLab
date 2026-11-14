using Domain;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> SearchAsync(string search);
}
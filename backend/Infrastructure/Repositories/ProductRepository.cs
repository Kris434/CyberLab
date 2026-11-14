using Application.Interfaces;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CyberLabDbContext _context;

    public ProductRepository(CyberLabDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> SearchAsync(string search)
    {
        return await _context.Products
            .AsNoTracking()
            .Where(p => p.Name.Contains(search))
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
}
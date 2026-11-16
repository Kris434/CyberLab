using Application.Scenarios.SqlInjection.Interfaces;
using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Scenarios.SqlInjection;

public class SqlInjectionRepository : ISqlInjectionRepository
{
    private readonly CyberLabDbContext _context;

    public SqlInjectionRepository(CyberLabDbContext context)
    {
        _context = context;
    }
    
    public Task<List<Product>> SearchVulnerableAsync(string search)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Product>> SearchSecureAsync(string search)
    {
        return await _context.Products
            .FromSqlInterpolated(
                $"SELECT * FROM \"Products\" WHERE \"Name\" ILIKE {'%' + search + '%'}")
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
}
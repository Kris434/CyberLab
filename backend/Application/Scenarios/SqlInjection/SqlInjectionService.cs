using Application.DTOs;
using Application.Scenarios.SqlInjection.DTOs;
using Application.Scenarios.SqlInjection.Interfaces;
using Domain;

namespace Application.Scenarios.SqlInjection;

public class SqlInjectionService
{
    private readonly ISqlInjectionRepository _repository;

    public SqlInjectionService(ISqlInjectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<SqlInjectionResultDto> SearchSecureAsync(
        string search)
    {
        var products = await _repository.SearchSecureAsync(search);

        var result = products
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price))
            .ToList();

        return new SqlInjectionResultDto(
            Mode: "Secure",
            Search: search,
            ResultCount: result.Count,
            Products: result
        );
    }
    
    public async Task<SqlInjectionResultDto> SearchVulnerableAsync(string search)
    {
        ValidateSearch(search);

        var products = await _repository.SearchVulnerableAsync(search);

        return CreateResult("Vulnerable", search, products);
    }
    
    private static void ValidateSearch(string search)
    {
        if (search.Length > 100)
        {
            throw new ArgumentException(
                "Search phrase cannot exceed 100 characters.");
        }
    }
    
    private static SqlInjectionResultDto CreateResult(
        string mode,
        string search,
        List<Product> products)
    {
        var result = products
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price))
            .ToList();

        return new SqlInjectionResultDto(
            Mode: mode,
            Search: search,
            ResultCount: result.Count,
            Products: result
        );
    }
}
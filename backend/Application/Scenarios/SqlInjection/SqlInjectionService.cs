using Application.DTOs;
using Application.Scenarios.SqlInjection.DTOs;
using Application.Scenarios.SqlInjection.Interfaces;

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
}
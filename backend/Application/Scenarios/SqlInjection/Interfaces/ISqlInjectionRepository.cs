using Domain;

namespace Application.Scenarios.SqlInjection.Interfaces;

public interface ISqlInjectionRepository
{
    Task<List<Product>> SearchVulnerableAsync(string search);

    Task<List<Product>> SearchSecureAsync(string search);
}
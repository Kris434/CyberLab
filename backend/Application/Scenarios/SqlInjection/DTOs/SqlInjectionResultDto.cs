using Application.DTOs;

namespace Application.Scenarios.SqlInjection.DTOs;

public record SqlInjectionResultDto(
    string Mode,
    string Search,
    int ResultCount,
    List<ProductDto> Products
    );
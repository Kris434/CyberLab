
using Application.Scenarios.SqlInjection.Interfaces;
using Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Scenarios.SqlInjection;

public class SqlInjectionRepository : ISqlInjectionRepository
{
    private readonly string _connectionString;

    public SqlInjectionRepository(IConfiguration configuration)
    {
        _connectionString = configuration
            .GetConnectionString("SqlInjectionDemo")
            ?? throw new InvalidOperationException(
                "SQL Injection demo database is not configured.");
    }

    // Secure version - parameterized query
    public async Task<List<Product>> SearchSecureAsync(string search)
    {
        const string sql = """
            SELECT "Id", "Name", "Price"
            FROM "Products"
            WHERE "Name" ILIKE '%' || @search || '%'
            ORDER BY "Id"
            """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("search", search);
        command.CommandTimeout = 3;

        return await ReadProductsAsync(command);
    }

    // Vulnerable version - intentionally unsafe SQL
    // FOR ISOLATED EDUCATIONAL USE ONLY
    public async Task<List<Product>> SearchVulnerableAsync(string search)
    {
        var sql = $"""
            SELECT "Id", "Name", "Price"
            FROM "Products"
            WHERE "Name" ILIKE '%{search}%'
            ORDER BY "Id"
            """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new NpgsqlCommand(sql, connection);

        command.CommandTimeout = 3;

        return await ReadProductsAsync(command);
    }

    private static async Task<List<Product>> ReadProductsAsync(
        NpgsqlCommand command)
    {
        var products = new List<Product>();

        await using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Price = reader.GetDecimal(2)
            });
        }

        return products;
    }
}

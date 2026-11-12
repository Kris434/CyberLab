using Application.DTOs;
using Application.Interfaces;
using Domain;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return users
            .Select(u => new UserDto(u.Id, u.Username))
            .ToList();
    }
}
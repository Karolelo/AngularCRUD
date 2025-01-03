using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Services;

public class UserServices
{
    private readonly IUserRepository _userRepository;
    
    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> CreateUser(CreateUserDto user)
    {
        return await _userRepository.CreateUser(user);
    }
    
    public async Task<bool> CheckUser(string username, string password)
    {
        var user = await _userRepository.GetUserByUsername(username);
        return user != null && user.Password == Helpers.PasswordHelpers.HashPassword(password);
    }

    public async Task<User> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsername(username);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }
}
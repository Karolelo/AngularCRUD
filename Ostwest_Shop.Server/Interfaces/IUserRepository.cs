using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);
    Task<User> CreateUser(CreateUserDto user);
}
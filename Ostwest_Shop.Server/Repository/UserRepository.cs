using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User> CreateUser(CreateUserDto user)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var newUser = new User()
            {
                Username = user.UserName,
                Password = Helpers.PasswordHelpers.HashPassword(user.Password)
            };
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var userData = new UserDatum()
            {
                UserId = newUser.Id,
                Name = user.Name,
                Surname = user.Surname,
                BirthDate = user.BirthDate,
                Email = user.Email,
            };
            await _context.AddAsync(userData);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            return newUser;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
    
}
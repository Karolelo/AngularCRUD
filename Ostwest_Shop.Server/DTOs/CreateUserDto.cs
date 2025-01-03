namespace Ostwest_Shop.Server.DTOs;

public class CreateUserDto
{
    public string UserName { get; set; }

    public string Password { get; set; }
    
    public string Email { get; set; }
    public string Name { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }
}
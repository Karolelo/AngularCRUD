using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.DTOs;

public class CreateProductDto
{
    public Product Product { get; set; }
    public List<Category> Categories { get; set; }
}
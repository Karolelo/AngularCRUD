using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.DTOs;

public class CreateProductDto
{
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public IFormFile? Img { get; set; }
        
        public int Quantity { get; set; }
        
        public List<int>? CategoriesIDs { get; set; }
    
}
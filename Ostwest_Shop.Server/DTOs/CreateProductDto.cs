using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.DTOs;

public class CreateProductDto
{
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public byte[]? Img { get; set; }

        public MagazineDto? Magazine { get; set; } 
        
        public List<int>? CategoriesIDs { get; set; }
    
}
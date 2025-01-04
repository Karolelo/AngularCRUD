namespace Ostwest_Shop.Server.DTOs;

public record UpdateProductDto(int Id,string Name,string Price,IFormFile? Img,int Quantity,List<int>? CategoriesIDs);
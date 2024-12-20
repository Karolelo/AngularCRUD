namespace Ostwest_Shop.Server.DTOs;

public record UpdateProductDto(int id,string Name,decimal price,byte[]? img,MagazineDto? magazine,List<int>? categoriesIDs);
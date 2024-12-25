namespace Ostwest_Shop.Server.DTOs;

public class PaginatedResponse <T>
{
    public List<T> Data { get; set; }
    public int Count { get; set; }
    
}
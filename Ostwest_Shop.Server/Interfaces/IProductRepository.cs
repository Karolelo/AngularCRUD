using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface IProductRepository
{
    Product GetById(int id);
    
    IEnumerable<Product> GetAll(bool includeMagazine,bool includeCategory);
    void CreateNewProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    
}
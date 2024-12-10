using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface IProductRepository
{
    Product GetById(int id);
    
    IEnumerable<Product> GetAll();
    void CreateNewProduct(Product product, List<Category> categories);
    public void UpdateProduct(Product product, List<Category> categories);
    public void DeleteProduct(Product product);
}
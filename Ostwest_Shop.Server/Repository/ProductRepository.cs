using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.DbContext;

namespace Ostwest_Shop.Server.Repository;

public class ProductRepository : IProductRepository
{
    private readonly Microsoft.EntityFrameworkCore.DbContext _context;

    public ProductRepository(Microsoft.EntityFrameworkCore.DbContext context)
    {
        _context = context;
    }

    public Product GetById(int id)
    {
        return _context.Set<Product>().Find(id) ?? throw new KeyNotFoundException("Product not found.");
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Set<Product>().ToList();
    }

    public void CreateNewProduct(Product product, List<Category> categories)
    {
        product.Categories = categories;
        _context.Add(product);
        _context.SaveChanges(); 
    }
    
    public void UpdateProduct(Product product, List<Category> categories)
    {
        product.Categories = categories;
        _context.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        _context.Remove(product);
        _context.SaveChanges();
    }
}
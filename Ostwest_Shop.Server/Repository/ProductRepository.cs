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

    public IEnumerable<Product> GetAll(bool includeMagazine,bool includeCategories)
    {
        if (includeMagazine)
        {
            if (includeCategories)
            {
                return _context.Set<Product>().
                    Include(p => p.Magazine)
                    .Include(p => p.Categories).ToList();
            }
            return _context.Set<Product>().Include(p =>p.Magazine).ToList();   
        }
        return _context.Set<Product>().ToList();
    }

    public void CreateNewProduct(Product product)
    {
        _context.Add(product);
        _context.SaveChanges(); 
    }
    
    public void UpdateProduct(Product product)
    {
        _context.Update(product);
        _context.SaveChanges();
    }

    public void DeleteProduct(Product product)
    {
        var deleteProduct = _context.Set<Product>()
            .Include(e=>e.Magazine)
            .Include(e=>e.Categories)
            .FirstOrDefault(p => p.Id == product.Id);
        
        _context.Remove(deleteProduct);
        _context.SaveChanges();
    }
}
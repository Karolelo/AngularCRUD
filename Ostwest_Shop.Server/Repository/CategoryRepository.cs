using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly Microsoft.EntityFrameworkCore.DbContext _context;

    public CategoryRepository(Microsoft.EntityFrameworkCore.DbContext context)
    {
        _context = context;
    }

    public Category getCategoryById(int id)
    {
        var category = _context.Set<Category>().Find(id);

        return category;
    }

    public ICollection<Category> getAllCategories()
    {
        return _context.Set<Category>().ToList();
    }

    public Category addCategory(string name)
    {
        var category = new Category()
        {
            CategoryName = name
        };
        _context.Add(category);
        _context.SaveChanges();
        return category;
    }

    public void deleteCategory(int id)
    {
        var category = _context.Set<Category>()
            .Include(p=>p.Products)
            .FirstOrDefault(p => p.Id == id);
        
        _context.Remove(category);
        _context.SaveChanges();
    }
}
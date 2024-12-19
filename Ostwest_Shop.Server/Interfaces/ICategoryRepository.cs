using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface ICategoryRepository
{
    Category getCategoryById(int id);
    ICollection<Category> getAllCategories();
    Category addCategory(string name);
    void deleteCategory(int id);
}
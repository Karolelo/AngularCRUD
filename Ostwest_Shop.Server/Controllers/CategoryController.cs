using Ostwest_Shop.Server.Repository;

namespace Ostwest_Shop.Server.Controllers;

public class CategoryController
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
}
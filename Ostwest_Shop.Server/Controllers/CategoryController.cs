using Microsoft.AspNetCore.Mvc;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.Repository;

namespace Ostwest_Shop.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Category>> GetAllCategories()
    {
        return Ok(_categoryRepository.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        return Ok(_categoryRepository.GetById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult<Category> DeleteCategory(int id)
    {
        var category = _categoryRepository.GetById(id);
        if (category == null)
            throw new ArgumentException($"No such category with id {id}");
        
        _categoryRepository.Delete(category);
        return Ok(category);
    }
}
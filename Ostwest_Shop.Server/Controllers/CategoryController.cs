using Microsoft.AspNetCore.Mvc;
using Ostwest_Shop.Server.DTOs;
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
        return Ok(_categoryRepository.getAllCategories());
    }
    
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        return Ok(_categoryRepository.getCategoryById(id));
    }

    [HttpDelete("{id}")]
    public ActionResult<Category> DeleteCategory(int id)
    {
        var category = _categoryRepository.getCategoryById(id);
        if (category == null)
            throw new ArgumentException($"No such category with id {id}");
        
        _categoryRepository.deleteCategory(id);
        return Ok(category);
    }

    [HttpPost]
    public ActionResult<Category> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        var createdCategory = _categoryRepository.addCategory(createCategoryDto.name);
    
        if (createdCategory == null)
        {
            return BadRequest("Nie udało się utworzyć kategorii");
        }
    
        return CreatedAtAction(
            nameof(GetCategoryById), 
            new { id = createdCategory.Id }, 
            createdCategory 
        );
    }
}
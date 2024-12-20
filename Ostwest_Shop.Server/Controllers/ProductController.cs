using Microsoft.AspNetCore.Mvc;
using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.Repository;

namespace Ostwest_Shop.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMagazineRepository _magazineRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly MyDbContext _dbContext;
    public ProductController(IProductRepository productRepository, IMagazineRepository magazineRepository,
        CategoryRepository categoryRepository, MyDbContext dbContext)
    {
        _productRepository = productRepository;
        _magazineRepository = magazineRepository;
        _categoryRepository = categoryRepository;
    }
    
    [HttpGet]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = _productRepository.GetById(id);
     
        if (product == null)
        {
            return NotFound(); 
        }
        
        return Ok(product); 
    }

    [HttpGet("all")]
    public ActionResult<IEnumerable<Product>> GetAllProducts()
    {
        return Ok(_productRepository.GetAll(includeMagazine: true,includeCategory: true));
    }
    
    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        Product product = new Product()
        {
            Name = createProductDto.Name,
            Price = createProductDto.Price,
            Img = createProductDto.Img,
        };
        
        

        if (createProductDto.CategoriesIDs != null && createProductDto.CategoriesIDs.Count > 0)
        {
            var categories = new List<Category>();
            foreach (var id in createProductDto.CategoriesIDs)
            {
                categories.Add(_categoryRepository.getCategoryById(id));
            }
            product.Categories = categories;
        }
        
        _productRepository.CreateNewProduct(product);
        
        if (createProductDto.Magazine != null)
        {
            Magazine magazine = new Magazine()
            {
                ProductId = product.Id, 
                Quanity = createProductDto.Magazine.Quantity
            };

            
            _magazineRepository.CreateMagazine(magazine.ProductId, magazine.Quanity);
        }
        
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut]
    public ActionResult<Product> UpdateProduct([FromBody] UpdateProductDto productDto)
    {
        _productRepository.UpdateProduct(productDto);

        if (productDto.magazine != null)
        {
            Magazine magazine = new Magazine()
            {
                ProductId = productDto.id,
                Quanity = productDto.magazine.Quantity
            };
            _magazineRepository.UpdateMagazine(magazine);
        }
        
        return Ok(new { message = $"Product został zaktualizowany {productDto.Name}" });
    }
    
    [HttpDelete("{id}")]
    public ActionResult<Product> DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound(); 
        }

        if (_magazineRepository.IsMagazineExist(id))
        {
            _magazineRepository.DeleteMagazine(id);
        }
        _productRepository.DeleteProduct(product);
        return Ok(product);
    }
    
    
    
}
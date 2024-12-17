using Microsoft.AspNetCore.Mvc;
using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IMagazineRepository _magazineRepository;
    
    public ProductController(IProductRepository productRepository, IMagazineRepository magazineRepository)
    {
        _productRepository = productRepository;
        _magazineRepository = magazineRepository;
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
            Img = createProductDto.Img
        };
        
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

    [HttpPut("id")]
    public ActionResult<Product> UpdateProduct( int id,[FromBody] Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }
        _productRepository.UpdateProduct(product);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult<Product> DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound(); 
        }
        _magazineRepository.DeleteMagazine(id);
        _productRepository.DeleteProduct(product);
        return Ok(product);
    }
    
}
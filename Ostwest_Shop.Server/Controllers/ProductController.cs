using Microsoft.AspNetCore.Mvc;
using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    IProductRepository _productRepository;
    
    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
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
        return Ok(_productRepository.GetAll());
    }
    
    [HttpPost]
    public ActionResult<Product> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        _productRepository.CreateNewProduct(createProductDto.Product, createProductDto.Categories);
        return CreatedAtAction(nameof(GetProduct), new { id = createProductDto.Product.Id }, createProductDto.Product);
    }

    [HttpDelete]
    public ActionResult<Product> DeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return NotFound(); 
        }
        _productRepository.DeleteProduct(product);
        return Ok(product);
    }
}
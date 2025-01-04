using System.Globalization;
using Microsoft.AspNetCore.Authorization;
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
        return Ok(_productRepository.GetAll(includeMagazine: true, includeCategory: true));
    }

    [HttpGet("page/{page}/size/{size}")]
    public IActionResult GetProducts(int page, int size)
    {
        var response = _productRepository.GetPaginatedProducts(page, size);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Product>> CreateProduct([FromForm] CreateProductDto productDto)
    {
        string relativeImgPath = null;

        if (productDto.Img != null)
        {
            var file = productDto.Img;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var absoluteImgPath = Path.Combine(uploadsFolder, uniqueFileName);
            relativeImgPath = $"/uploads/{uniqueFileName}";


            using (var stream = new FileStream(absoluteImgPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        decimal priceDecimal;
        if (!Decimal.TryParse(productDto.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out priceDecimal))
        {
            return BadRequest($"Invalid price format: {productDto.Price}");
        }

        Product product = new Product()
        {
            Name = productDto.Name,
            Price = priceDecimal,
            ImgSourcePath = relativeImgPath
        };

        if (productDto.CategoriesIDs != null && productDto.CategoriesIDs.Count > 0)
        {
            var categories = new List<Category>();
            foreach (var id in productDto.CategoriesIDs)
            {
                categories.Add(_categoryRepository.getCategoryById(id));
            }

            product.Categories = categories;
        }

        _productRepository.CreateNewProduct(product);
        _magazineRepository.CreateMagazine(product.Id, productDto.Quantity);
        

        string imageUrl = relativeImgPath != null
            ? $"{Request.Scheme}://{Request.Host}{relativeImgPath}"
            : null;

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, new
        {
            id = product.Id,
            name = product.Name,
            price = product.Price,
            imageUrl
        });
    }

    [HttpPut]
    [Authorize(Roles = "admin")]
    public ActionResult<Product> UpdateProduct([FromForm] UpdateProductDto productDto)
    {
        string relativeImgPath = null;
        
        Magazine magazine = new Magazine()
        {
            ProductId = productDto.Id,
            Quanity = productDto.Quantity
        };
        _magazineRepository.UpdateMagazine(magazine);

        if (productDto.Img != null)
        {
            var file = productDto.Img;
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var absoluteImgPath = Path.Combine(uploadsFolder, uniqueFileName);
            relativeImgPath = $"/uploads/{uniqueFileName}";


            using (var stream = new FileStream(absoluteImgPath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }
        }
        _productRepository.UpdateProduct(productDto, relativeImgPath);
        return Ok(new { message = $"Product został zaktualizowany {productDto.Name}" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
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
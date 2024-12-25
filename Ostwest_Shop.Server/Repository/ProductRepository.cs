using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;
using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.DTOs;

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

    public IEnumerable<Product> GetAll(bool includeMagazine, bool includeCategories)
    {
        if (includeMagazine)
        {
            if (includeCategories)
            {
                return _context.Set<Product>().Include(p => p.Magazine)
                    .Include(p => p.Categories).ToList();
            }

            return _context.Set<Product>().Include(p => p.Magazine).ToList();
        }

        return _context.Set<Product>().ToList();
    }
    
    public PaginatedResponse<Product> GetPaginatedProducts(int page, int pageSize)
    {
        var count = _context.Set<Product>().Count();
        var data = _context.Set<Product>()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return new PaginatedResponse<Product>
        {
            Data = data,
            Count = count
        };
    }

    public void CreateNewProduct(Product product)
    {
        _context.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(UpdateProductDto product)
{
    using (var transaction = _context.Database.BeginTransaction())
    {
        try
        {
            var existingProduct = GetById(product.id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {product.id} does not exist.");
            }
            
            string updateProductSql = @"
            UPDATE Product
            SET Name = @Name, Price = @Price
            WHERE ID = @ProductId";

            _context.Database.ExecuteSqlRaw(updateProductSql,
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Price", product.price),
                new SqlParameter("@ProductId", product.id));
            
            string deleteProductCategorySql = @"
            DELETE FROM Product_category
            WHERE Product_ID = @ProductId";

            _context.Database.ExecuteSqlRaw(deleteProductCategorySql,
                new SqlParameter("@ProductId", product.id));
            
            if (product.categoriesIDs != null && product.categoriesIDs.Count > 0)
            {
                foreach (var categoryId in product.categoriesIDs)
                {
                    string insertProductCategorySql = @"
                    INSERT INTO Product_category (Category_ID, Product_ID)
                    VALUES (@CategoryId, @ProductId)";

                    _context.Database.ExecuteSqlRaw(insertProductCategorySql,
                        new SqlParameter("@CategoryId", categoryId),
                        new SqlParameter("@ProductId", product.id));
                }
            }
            
            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw new Exception("Error updating product and its categories.", ex);
        }
    }
}

    public void DeleteProduct(Product product)
    {
        var deleteProduct = _context.Set<Product>()
            .Include(e => e.Magazine)
            .Include(e => e.Categories)
            .FirstOrDefault(p => p.Id == product.Id);

        _context.Remove(deleteProduct);
        _context.SaveChanges();
    }
}
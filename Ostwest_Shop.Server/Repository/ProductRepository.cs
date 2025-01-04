using System.Globalization;
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
                return _context.Set<Product>()
                    .Include(e => e.Categories)
                    .Include(e => e.Magazine).ToList();
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

    public void UpdateProduct(UpdateProductDto product, string relativePath)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var existingProduct = GetById(product.Id);
                if (existingProduct == null)
                {
                    throw new Exception($"Product with ID {product.Id} does not exist.");
                }

                string updateProductSql = @"
            UPDATE Product
            SET Name = @Name, Price = @Price
            WHERE ID = @ProductId";
                decimal priceDecimal;
                if (!Decimal.TryParse(product.Price, NumberStyles.Any, CultureInfo.InvariantCulture, out priceDecimal))
                {
                    throw new Exception($"Invalid price format: {product.Price}");
                }
                _context.Database.ExecuteSqlRaw(updateProductSql,
                    new SqlParameter("@Name", product.Name),
                    new SqlParameter("@Price", priceDecimal),
                    new SqlParameter("@ProductId", product.Id));

                string deleteProductCategorySql = @"
            DELETE FROM Product_category
            WHERE Product_ID = @ProductId";

                _context.Database.ExecuteSqlRaw(deleteProductCategorySql,
                    new SqlParameter("@ProductId", product.Id));

                if (product.CategoriesIDs != null && product.CategoriesIDs.Count > 0)
                {
                    foreach (var categoryId in product.CategoriesIDs)
                    {
                        string insertProductCategorySql = @"
                    INSERT INTO Product_category (Category_ID, Product_ID)
                    VALUES (@CategoryId, @ProductId)";

                        _context.Database.ExecuteSqlRaw(insertProductCategorySql,
                            new SqlParameter("@CategoryId", categoryId),
                            new SqlParameter("@ProductId", product.Id));
                    }
                }

                if (relativePath != null)
                {
                    string updateProductImageSql = @"
                UPDATE Product
                SET img_source_path = @img_source_path
                WHERE ID = @ProductId";

                    _context.Database.ExecuteSqlRaw(updateProductImageSql,
                        new SqlParameter("@img_source_path", relativePath),
                        new SqlParameter("@ProductId", product.Id));
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
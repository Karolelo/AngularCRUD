﻿using Ostwest_Shop.Server.DTOs;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface IProductRepository
{
    Product GetById(int id);
    IEnumerable<Product> GetAll(bool includeMagazine,bool includeCategory);
    PaginatedResponse<Product> GetPaginatedProducts(int page, int pageSize);
    void CreateNewProduct(Product product);
    void UpdateProduct(UpdateProductDto product,string relativePath);
    void DeleteProduct(Product product);
    
    
}
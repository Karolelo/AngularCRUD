using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Repository;

public class CategoryRepository : EntityFrameworkRepository<Category>
{
    public CategoryRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
    {
    }
    
}
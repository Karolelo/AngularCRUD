using Ostwest_Shop.Server.DbContext;
using Ostwest_Shop.Server.Interfaces;
using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Repository;

public class MagazineRepository : IMagazineRepository
{
    private readonly MyDbContext _context;

    public MagazineRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public int getMagazineQuantity(int productId)
    {
        return _context.Magazines.Find(productId).Quanity;
    }

    public bool IsMagazineExist(int magazineId)
    {
        return _context.Magazines.Any(m => m.ProductId == magazineId);
    }

    public void CreateMagazine(int productId, int quantity)
    {
        Magazine magazine = new Magazine()
        {
            ProductId = productId,
            Quanity = quantity
        };
        _context.Add(magazine);
        _context.SaveChanges();
    }

    public void UpdateMagazine(Magazine magazine)
    {
        if (!_context.Magazines.Any(m => m.ProductId == magazine.ProductId))
        {
            throw new ArgumentException($"Magazine with ID {magazine.ProductId} does not exist.");
        }

        _context.Magazines.Update(magazine);
        _context.SaveChanges();
    }

    public void DeleteMagazine(int productId)
    {
        var magazine = _context.Magazines.Find(productId);
        
        if(magazine == null)
        {
            throw new ArgumentException($"Magazine with ID {productId} does not exist.");
        }
        _context.Magazines.Remove(magazine);
        _context.SaveChanges();
    }
}
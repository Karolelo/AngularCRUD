using Ostwest_Shop.Server.Models;

namespace Ostwest_Shop.Server.Interfaces;

public interface IMagazineRepository
{ 
        bool IsMagazineExist(int magazineId);
        void CreateMagazine(int productId, int quantity);
        void UpdateMagazine(Magazine magazine);
        void DeleteMagazine(int productId);
}
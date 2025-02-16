using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISepetRepository
    {
        Task<Siparis> GetByUserIdAsync(int userId);
        Task<bool> AddItemAsync(int userId, int productId, int quantity);
        Task<bool> RemoveItemAsync(int userId, int productId);
        Task<bool> ClearCartAsync(int userId);
        Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity);
    }
}

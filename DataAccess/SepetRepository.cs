using Dapper;
using Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SepetRepository : ISepetRepository
    {
        private readonly DapperDbConnection _db;

        public SepetRepository(DapperDbConnection connection)
        {
            _db = connection;
        }

        public async Task<Siparis> GetByUserIdAsync(int userId)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "SELECT * FROM Cart WHERE UserId = @UserId";
                return await conn.QueryFirstOrDefaultAsync<Siparis>(query, new { UserId = userId });
            }
        }

        public async Task<bool> AddItemAsync(int userId, int productId, int quantity)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "INSERT INTO CartItem (CartId, ProductId, Quantity) VALUES (@CartId, @ProductId, @Quantity)";
                var cart = await GetByUserIdAsync(userId);
                if (cart == null) return false;

                var result = await conn.ExecuteAsync(query, new { CartId = cart.Id, ProductId = productId, Quantity = quantity });
                return result > 0;
            }
        }

        public async Task<bool> RemoveItemAsync(int userId, int productId)
        {
            using (var conn = _db.CreateConnection())
            {
                var cart = await GetByUserIdAsync(userId);
                if (cart == null) return false;

                var query = "DELETE FROM CartItem WHERE CartId = @CartId AND ProductId = @ProductId";
                var result = await conn.ExecuteAsync(query, new { CartId = cart.Id, ProductId = productId });
                return result > 0;
            }
        }

        public async Task<bool> ClearCartAsync(int userId)
        {
            using (var conn = _db.CreateConnection())
            {
                var cart = await GetByUserIdAsync(userId);
                if (cart == null) return false;

                var query = "DELETE FROM CartItem WHERE CartId = @CartId";
                var result = await conn.ExecuteAsync(query, new { CartId = cart.Id });
                return result > 0;
            }
        }

        public async Task<bool> UpdateQuantityAsync(int userId, int productId, int quantity)
        {
            using (var conn = _db.CreateConnection())
            {
                var cart = await GetByUserIdAsync(userId);
                if (cart == null) return false;

                var query = "UPDATE CartItem SET Quantity = @Quantity WHERE CartId = @CartId AND ProductId = @ProductId";
                var result = await conn.ExecuteAsync(query, new { CartId = cart.Id, ProductId = productId, Quantity = quantity });
                return result > 0;
            }
        }
    }
}

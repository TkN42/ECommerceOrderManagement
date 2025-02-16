using Dapper;
using Domain;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UrunRepository : IUrunRepository
    {
        private readonly DapperDbConnection _db;

        public UrunRepository(DapperDbConnection connection)
        {
            _db = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public async Task<Urun> GetByIdAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "SELECT * FROM Urun WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Urun>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Urun>> GetAllAsync()
        {
            using (var conn = _db.CreateConnection())
            {
                //conn.Open();
                var query = "SELECT * FROM Urun";
                //conn.Close();
                return await conn.QueryAsync<Urun>(query);
            }
        }

        public async Task<int> AddAsync(Urun product)
        {
            try
            {
                using (var conn = _db.CreateConnection())
                {
                    //conn.Open();
                    var query = "INSERT INTO Urun (UrunKodu, UrunAdi, Fiyat) VALUES (@UrunKodu, @UrunAdi, @Fiyat)";
                    //conn.Close();
                    return await conn.ExecuteAsync(query, product);
                }
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public async Task<bool> UpdateAsync(Urun product)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "UPDATE Urun SET UrunKodu = @UrunKodu, UrunAdi = @UrunAdi, Fiyat = @Fiyat WHERE Id = @Id";
                var result = await conn.ExecuteAsync(query, product);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "DELETE FROM Urun WHERE Id = @Id";
                var result = await conn.ExecuteAsync(query, new { Id = id });
                return result > 0;
            }
        }

        public async Task<IEnumerable<Urun>> GetByCategoryAsync(string category)
        {
            using (var conn = _db.CreateConnection())
            {
                var query = "SELECT * FROM Urun WHERE Category = @Category";
                return await conn.QueryAsync<Urun>(query, new { Category = category });
            }
        }
    }
}

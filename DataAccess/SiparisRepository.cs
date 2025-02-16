using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
namespace DataAccess
{
    public class SiparisRepository : ISiparisRepository
    {
        private readonly DapperDbConnection _db;

        public SiparisRepository(DapperDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateSiparisAsync(int kullaniciId, int urunId, int miktar, decimal fiyat)
        {
            using (var conn = _db.CreateConnection())
            {
                if (conn == null)
                    throw new Exception("Veritabanı bağlantısı NULL dönüyor!");
                try
                {
                    string siparisQuery = "INSERT INTO Siparis (SiparisTarihi, KullaniciId) OUTPUT INSERTED.Id VALUES (@SiparisTarihi, @KullaniciId)";
                    int siparisId = await conn.ExecuteScalarAsync<int>(siparisQuery, new { SiparisTarihi = DateTime.Now, KullaniciId = kullaniciId });

                    string detayQuery = "INSERT INTO SiparisDetay (SiparisId, UrunId, Miktar, Fiyat) VALUES (@SiparisId, @UrunId, @Miktar, @Fiyat)";
                    await conn.ExecuteAsync(detayQuery, new { SiparisId = siparisId, UrunId = urunId, Miktar = miktar, Fiyat = fiyat });

                    return siparisId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }

        public async Task<Siparis> GetByIdAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "SELECT * FROM Siparis WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Siparis>(query, new { Id = id });
            }
        }

        public async Task<IEnumerable<Siparis>> GetAllAsync()
        {
            using (var conn = _db.CreateConnection())
            {
                string query = @"
            SELECT s.Id, s.SiparisTarihi, s.KullaniciId
            FROM Siparis s
            JOIN SiparisDetay sd ON s.Id = sd.SiparisId";

                return await conn.QueryAsync<Siparis>(query);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "DELETE FROM Siparis WHERE Id = @Id";
                int rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<IEnumerable<SiparisDetay>> GetSiparisDetailsAsync(int siparisId)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = @"
            SELECT sd.Id, sd.SiparisId, sd.UrunId, ur.UrunAdi, sd.Miktar, sd.Fiyat
            FROM SiparisDetay sd
            JOIN Urun ur ON sd.UrunId = ur.Id
            WHERE sd.SiparisId = @SiparisId";

                return await conn.QueryAsync<SiparisDetay>(query, new { SiparisId = siparisId });
            }
        }

    }
}
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace DataAccess
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly DapperDbConnection _db;

        public KullaniciRepository(DapperDbConnection db)
        {
            _db = db;
        }

        public async Task<Kullanici> GetByIdAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "SELECT * FROM Kullanici WHERE Id = @Id";
                return await conn.QueryFirstOrDefaultAsync<Kullanici>(query, new { Id = id });
            }
        }

        public async Task<Kullanici> GetByUsernameAsync(string kullaniciAdi)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "SELECT * FROM Kullanici WHERE KullaniciAdi = @KullaniciAdi";
                return await conn.QueryFirstOrDefaultAsync<Kullanici>(query, new { KullaniciAdi = kullaniciAdi });
            }
        }

        public async Task<IEnumerable<Kullanici>> GetAllAsync()
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "SELECT * FROM Kullanici";
                return await conn.QueryAsync<Kullanici>(query);
            }
        }

        public async Task<int> AddAsync(Kullanici kullanici)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "INSERT INTO Kullanici (KullaniciAdi, Parola) VALUES (@KullaniciAdi, @Parola); SELECT CAST(SCOPE_IDENTITY() as int)";
                return await conn.ExecuteScalarAsync<int>(query, kullanici);
            }
        }

        public async Task<bool> UpdateAsync(Kullanici kullanici)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "UPDATE Kullanici SET KullaniciAdi = @KullaniciAdi, Parola = @Parola WHERE Id = @Id";
                int rowsAffected = await conn.ExecuteAsync(query, kullanici);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "DELETE FROM Kullanici WHERE Id = @Id";
                int rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }

        public async Task<bool> ChangePasswordAsync(int id, string newPassword)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "UPDATE Kullanici SET Parola = @Parola WHERE Id = @Id";
                int rowsAffected = await conn.ExecuteAsync(query, new { Id = id, Parola = newPassword });
                return rowsAffected > 0;
            }
        }

        public async Task<bool> ValidateUserAsync(string kullaniciAdi, string parola)
        {
            using (var conn = _db.CreateConnection())
            {
                string query = "SELECT COUNT(1) FROM Kullanici WHERE KullaniciAdi = @KullaniciAdi AND Parola = @Parola";
                int count = await conn.ExecuteScalarAsync<int>(query, new { KullaniciAdi = kullaniciAdi, Parola = parola });
                return count > 0;
            }
        }
    }
}

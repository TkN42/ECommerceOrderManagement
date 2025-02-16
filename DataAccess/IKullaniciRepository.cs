using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IKullaniciRepository
    {
        Task<Kullanici> GetByIdAsync(int id);
        Task<Kullanici> GetByUsernameAsync(string kullaniciAdi);
        Task<IEnumerable<Kullanici>> GetAllAsync();
        Task<int> AddAsync(Kullanici kullanici);
        Task<bool> UpdateAsync(Kullanici kullanici);
        Task<bool> DeleteAsync(int id);
        Task<bool> ChangePasswordAsync(int id, string newPassword);
        Task<bool> ValidateUserAsync(string kullaniciAdi, string parola);
    }
}
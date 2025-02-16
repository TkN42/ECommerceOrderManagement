using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISiparisRepository
    {
        Task<int> CreateSiparisAsync(int kullaniciId, int urunId, int miktar, decimal fiyat);
        Task<Siparis> GetByIdAsync(int id);
        Task<IEnumerable<Siparis>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<SiparisDetay>> GetSiparisDetailsAsync(int siparisId);
    }
}

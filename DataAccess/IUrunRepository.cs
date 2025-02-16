using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IUrunRepository
    {
        Task<Urun> GetByIdAsync(int id);
        Task<IEnumerable<Urun>> GetAllAsync();
        Task<int> AddAsync(Urun product);
        Task<bool> UpdateAsync(Urun product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Urun>> GetByCategoryAsync(string category);
    }
}

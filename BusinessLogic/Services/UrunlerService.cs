using DataAccess;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class UrunlerService
    {
        private readonly IUrunRepository _urunRepo;

        public UrunlerService(IUrunRepository urunRepo)
        {
            _urunRepo = urunRepo;
        }
        public async Task<Urun> GetProductByIdAsync(int id)
        {
            try
            {
                return await _urunRepo.GetByIdAsync(id); 
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Urun>> GetAllProductsAsync()
        {
            return await _urunRepo.GetAllAsync();
        }

        public async Task<int> AddProductAsync(Urun product)
        {
            return await _urunRepo.AddAsync(product);
        }

        public async Task<bool> UpdateProductAsync(Urun product)
        {
            return await _urunRepo.UpdateAsync(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _urunRepo.DeleteAsync(id);
        }
    }
}

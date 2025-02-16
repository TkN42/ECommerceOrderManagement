using BusinessLogic.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrunController : ControllerBase
    {
        private readonly UrunlerService _urunlerService;

        private readonly ILogger<UrunController> _logger;

        public UrunController(UrunlerService urunlerService, ILogger<UrunController> logger)
        {
            _urunlerService = urunlerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Urun>>> GetAllProducts()
        {
            var products = await _urunlerService.GetAllProductsAsync();
            if (products == null || !products.Any())
                return NotFound("Ürün bulunamadı.");

            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddProduct([FromBody] Urun product)
        {
            if (product == null)
            {
                _logger.LogWarning("Ürün bilgisi null geldi.");
                return BadRequest("Ürün bilgisi eksik.");
            }

            try
            {
                var productId = await _urunlerService.AddProductAsync(product);
                _logger.LogInformation($"Yeni ürün eklendi. ID: {productId}");
                return Ok(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün eklenirken hata oluştu.");
                return StatusCode(500, "Ürün eklenirken bir hata meydana geldi.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProduct([FromBody] Urun product)
        {
            if (product == null || product.Id <= 0)
                return BadRequest("Geçersiz ürün bilgisi.");

            var existingProduct = await _urunlerService.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
                return NotFound("Güncellenecek ürün bulunamadı.");

            var result = await _urunlerService.UpdateProductAsync(product);
            if (!result)
                return StatusCode(500, "Ürün güncellenirken bir hata oluştu.");

            return Ok("Ürün güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (id <= 0)
                return BadRequest("Geçersiz ürün ID.");

            var existingProduct = await _urunlerService.GetProductByIdAsync(id);
            if (existingProduct == null)
                return NotFound("Silinecek ürün bulunamadı.");

            var result = await _urunlerService.DeleteProductAsync(id);
            if (!result)
                return StatusCode(500, "Ürün silinirken bir hata oluştu.");

            return Ok("Ürün başarıyla silindi.");
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var p  = await _repo.GetProductBrandsAsync();
            return Ok(p);
        }

        [HttpGet]
        public async Task<ActionResult<ProductBrand>> GetProductBrands(int id)
        {
            return await _repo.GetProductBrandByIdAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var p = await _repo.GetProductTypesAsync();
            return Ok(p);
        }

        [HttpGet]
        public async Task<ActionResult<ProductType>> GetProductTypes(int id)
        {
            return await _repo.GetProductTypeByIdAsync(id);
        }

      [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var p = await _repo.GetProductsAsync();
            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }

    }
}
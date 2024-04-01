using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {   
        
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<Product> _productTypeRepo;
        private readonly IGenericRepository<Product> _productBrandRepo;

        public ProductsController(IGenericRepository<Product> productRepo,
            IGenericRepository<Product> productTypeRepo,IGenericRepository<Product> productBrandRepo)
        {
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productRepo = productRepo;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var p = await _productBrandRepo.ListAllAsync();
            return Ok(p);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var p = await _productTypeRepo.ListAllAsync();
            return Ok(p);
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            
            var p = await _productRepo.ListAsync(spec);

            return Ok(p);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            return await _productRepo.GetEntityWithSpec(spec);
        }

    }
}
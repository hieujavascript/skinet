using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // private readonly StoreContext _context;
        private readonly IRepositoryProduct _repositoryProduct;
        public ProductsController(IRepositoryProduct repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> getProducts()
        {
            var products = await _repositoryProduct.getProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getProduct(int id)
        {
            var product =  await _repositoryProduct.getProductIdAsync(id);
            return product;
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrands() {
            var productbrands = await _repositoryProduct.getProductBrandsAsync();
            return productbrands;

        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> getProductTypes(){
            var productTypes = await _repositoryProduct.getProductTypesAsync();
            return productTypes;
        }
    }
}
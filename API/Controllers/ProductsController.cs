using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using AutoMapper.Configuration.Conventions;
using Core.Entity;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repoProduct;
        private readonly IGenericRepository<ProductBrand> _repoProductBrand;
        private readonly IGenericRepository<ProductType> _repoProductType;
        private readonly IMapper _mapper;

        // private readonly StoreContext _context;
        public ProductsController(IGenericRepository<Product> repoProduct,
        IGenericRepository<ProductBrand> repoProductBrand ,
         IGenericRepository<ProductType> repoProductType ,
         IMapper mapper
        )
        {
            this._repoProduct = repoProduct;
            this._repoProductBrand = repoProductBrand;
            this._repoProductType = repoProductType;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> getProducts()
        {
           // var products = await _repoProduct.ListAllAsync();
           var spec = new ProductsWithTypesAndBrandSpecication();           
           var products = await _repoProduct.ListAsync(spec);
           //var productdto = products.AsQueryable().ProjectTo<ProductToReturnDto>(_mapper.ConfigurationProvider).ToList();
           var productdto =  _mapper.Map<List<Product> , List<ProductToReturnDto>>(products);
           return productdto;    
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> getProduct(int id)
        {
           // var product =  await _repoProduct.getByIdAsync(id);
           var spec = new ProductsWithTypesAndBrandSpecication(id);
           var product = await _repoProduct.GetEntityWithSpec(spec);
            
            return _mapper.Map<Product , ProductToReturnDto>(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> getProductBrands() {
            var productbrands = await _repoProductBrand.ListAllAsync();
            return productbrands;

        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> getProductTypes(){
            var productTypes = await _repoProductType.ListAllAsync();
            return productTypes;
        }
    }
}
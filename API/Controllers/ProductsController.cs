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
using Microsoft.AspNetCore.Http;
using API.Error;
using Core;
using API.helper;

namespace API.Controllers
{
    
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> getProducts([FromQuery]ProductSpecParams productSpecParams)
        {
           // var products = await _repoProduct.ListAllAsync();
           var spec = new ProductsWithTypesAndBrandSpecication(productSpecParams);
           // đếm toàn bộ dữ liệu ko hề có sort , filter                      
           var countSpect = new ProductWithFiltersForCountSpecification(productSpecParams);
           var totalItems = await _repoProduct.CountAsync(countSpect);

           var products = await _repoProduct.ListAsync(spec);
           //var productdto = products.AsQueryable().ProjectTo<ProductToReturnDto>(_mapper.ConfigurationProvider).ToList();
           var data =  _mapper.Map<List<Product> , List<ProductToReturnDto>>(products);
           var dataPagination = new Pagination<ProductToReturnDto>(productSpecParams.PageIndex , 
           productSpecParams.PageSize , totalItems  , data
           );
           return Ok(dataPagination);    
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> getProduct(int id)
        {
           // var product =  await _repoProduct.getByIdAsync(id);
           var spec = new ProductsWithTypesAndBrandSpecication(id);
           var product = await _repoProduct.GetEntityWithSpec(spec);
            if(product == null)
            return NotFound(new ApiResponse(404));

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
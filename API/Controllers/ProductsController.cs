using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Errors;
using API.Helper;
using AutoMapper;
using Core.Dto;
using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  
    public class ProductsController : ApiBaseController
    {
        
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<ProductBrand> brandRepository,
            IGenericRepository<ProductType> productTypeRepository,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductOutputDto>>> GetProducts([FromQuery]ProductInputDto input)
        {
            var spec = new ProductWithTypeAndBrandSpecification(input);
            var countSpec = new ProductWithFilterForCountSpecification(input);
            var count = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.ListAsync(spec);            
            var data = _mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductOutputDto>>(products);
            return Ok(new Pagination<ProductOutputDto>(input.PageIndex,input.PageSize,count,data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode:StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponseModel),statusCode:StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _productRepository.GetEntityWithSpec(spec);
            if (product == null ) return NotFound(new ApiResponseModel(404));
            return Ok(_mapper.Map<Product,ProductOutputDto>(product));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.ListAllAsync();
            return Ok(productTypes);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _brandRepository.ListAllAsync();
            return Ok(productBrands);
        }

    }
}
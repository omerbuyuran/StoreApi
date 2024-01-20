using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;
using StoreApi.Domain.Request;
using StoreApi.Domain.Responses;
using StoreApi.Extensions;

namespace StoreApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ProductListResponse productListResponse = await productService.ListAsync();
            if (productListResponse.Success)
            {
                return Ok(productListResponse.ProductList);
            }
            else
            {
                return BadRequest(productListResponse.Message);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            ProductResponse productResponse = await productService.GetProductByIdAsync(id);
            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductRequest productRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductRequest,Product>(productRequest);
                ProductResponse productResponse = await productService.AddProduct(product);
                if (productResponse.Success)
                {
                    return Ok(productResponse.Product);
                }
                else
                {
                    return BadRequest(productResponse.Message);
                }
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductRequest productRequest,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                Product product = mapper.Map<ProductRequest, Product>(productRequest);
                ProductResponse productResponse = await productService.UpdateProduct(product,id);
                if (productResponse.Success)
                {
                    return Ok(productResponse.Product);
                }
                else
                {
                    return BadRequest(productResponse.Message);
                }
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            ProductResponse response = await productService.RemoveProduct(id);
            if (response.Success)
            {
                return Ok(response.Product);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}

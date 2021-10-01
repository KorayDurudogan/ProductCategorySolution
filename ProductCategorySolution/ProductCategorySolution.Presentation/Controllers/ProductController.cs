using Microsoft.AspNetCore.Mvc;
using ProductCategory.Service.Models;
using ProductCategory.Services.ProductServices;
using ProductCategorySolution.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategorySolution.Controllers
{
    public class ProductController : HepsiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) => _productService = productService;

        [HttpGet, Route("all")]
        public async Task<IEnumerable<ProductRequestDto>> Get() => await _productService.GetAllAsync();

        [HttpGet]
        public async Task<ProductRequestDto> Get(string id) => await _productService.GetAsync(id);

        [HttpPost]
        public async Task Post(ProductRequestDto product) => await _productService.InsertAsync(product);

        [HttpPut]
        public async Task Put(string id, ProductRequestDto product) => await _productService.UpdateAsync(id, product);

        [HttpDelete]
        public async Task Delete(string id) => await _productService.DeleteAsync(id);

        [HttpGet, Route("filter/name={name}&categoryId={categoryId}")]
        public async Task<IEnumerable<ProductRequestDto>> Filter(string name, string categoryId) => await _productService.FilterAsync(name, categoryId);

        [HttpGet, Route("filter-name/{name}")]
        public async Task<IEnumerable<ProductRequestDto>> FilterByName(string name) => await _productService.FilterAsync(name);

        [HttpGet, Route("filter-category/{categoryId}")]
        public async Task<IEnumerable<ProductRequestDto>> FilterByCategory(string categoryId) => await _productService.FilterAsync(string.Empty, categoryId);
    }
}
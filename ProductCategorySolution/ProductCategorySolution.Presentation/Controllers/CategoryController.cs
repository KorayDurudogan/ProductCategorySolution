using Microsoft.AspNetCore.Mvc;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.Models;
using ProductCategorySolution.Presentation.Controllers.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategorySolution.Presentation.Controllers
{
    public class CategoryController : HepsiController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet, Route("all")]
        public async Task<IEnumerable<CategoryResponseDto>> Get() => await _categoryService.GetAll();

        [HttpGet]
        public async Task<CategoryResponseDto> Get(string id) => await _categoryService.GetAsync(id);

        [HttpPost]
        public async Task Post(CategoryRequestDto product) => await _categoryService.InsertAsync(product);

        [HttpPut]
        public async Task Put(string id, CategoryRequestDto product) => await _categoryService.UpdateAsync(id, product);

        [HttpDelete]
        public async Task Delete(string id) => await _categoryService.DeleteAsync(id);

        [HttpGet, Route("filter-name/{name}")]
        public async Task<IEnumerable<CategoryResponseDto>> FilterByName(string name) => await _categoryService.FilterAsync(name);
    }
}

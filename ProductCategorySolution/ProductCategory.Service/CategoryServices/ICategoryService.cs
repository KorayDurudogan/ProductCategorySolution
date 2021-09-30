using ProductCategory.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategory.Service.CategoryServices
{
    public interface ICategoryService
    {
        Task<CategoryResponseDto> GetAsync(string id);

        Task InsertAsync(CategoryRequestDto categoryDto);

        Task UpdateAsync(string id, CategoryRequestDto categoryDto);

        Task DeleteAsync(string id);

        Task<IEnumerable<CategoryResponseDto>> FilterAsync(string name);

        Task<IEnumerable<CategoryResponseDto>> GetAll();
    }
}

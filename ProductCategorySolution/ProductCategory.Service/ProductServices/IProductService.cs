using ProductCategory.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategory.Services.ProductServices
{
    /// <summary>
    /// An interface for product processes.
    /// </summary>
    public interface IProductService
    {
        Task<ProductResponseDto> GetAsync(string id);

        Task InsertAsync(ProductRequestDto product);

        Task UpdateAsync(string id, ProductRequestDto product);

        Task DeleteAsync(string id);

        Task<IEnumerable<ProductResponseDto>> FilterAsync(string name = null, string category = null);

        Task<IEnumerable<ProductResponseDto>> GetAll();
    }
}

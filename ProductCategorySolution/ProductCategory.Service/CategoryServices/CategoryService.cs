using AutoMapper;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategory.Service.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IDataDao<Category> _dataDao;

        private readonly IMapper _mapper;

        public CategoryService(IDataDao<Category> dataDao, IMapper mapper)
        {
            _dataDao = dataDao;
            _mapper = mapper;
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();

            await _dataDao.DeleteAsync(id);
        }

        public async Task<CategoryResponseDto> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();

            var product = await _dataDao.GetAsync(id);
            return _mapper.Map<CategoryResponseDto>(product);
        }

        public async Task InsertAsync(CategoryRequestDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto?.Name))
                throw new ArgumentNullException();

            var category = _mapper.Map<Category>(categoryDto);
            await _dataDao.InsertAsync(category);
        }

        public async Task UpdateAsync(string id, CategoryRequestDto categoryDto)
        {
            if (string.IsNullOrWhiteSpace(categoryDto?.Name))
                throw new ArgumentNullException();

            var category = _mapper.Map<Category>(categoryDto);
            await _dataDao.UpdateAsync(id, category);
        }

        public async Task<IEnumerable<CategoryResponseDto>> FilterAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            var categories = await _dataDao.FilterAsync(p => p.Name.Contains(name));
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAll()
        {
            var categories = await _dataDao.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        }
    }
}

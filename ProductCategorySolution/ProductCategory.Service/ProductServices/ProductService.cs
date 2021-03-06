using AutoMapper;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Infrastructure.Redis;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.Extensions;
using ProductCategory.Service.Models;
using ProductCategory.Services.ProductServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCategory.Service.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IDataDao<Product> _dataDao;

        private readonly IMapper _mapper;

        private readonly ICategoryService _categoryService;

        private readonly IRedisManager _redisManager;

        public ProductService(IDataDao<Product> dataDao, IMapper mapper, ICategoryService categoryService, IRedisManager redisManager)
        {
            _dataDao = dataDao;
            _mapper = mapper;
            _categoryService = categoryService;
            _redisManager = redisManager;
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();

            await _dataDao.DeleteAsync(id);
        }

        public async Task<ProductResponseDto> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException();

            string redisKey = $"product-{id}";

            var product = _redisManager.Get<ProductResponseDto>(redisKey);
            if (product == null)
            {
                var selectedProductproduct = await _dataDao.GetAsync(id);
                var productResponseDto = _mapper.Map<ProductResponseDto>(selectedProductproduct);

                productResponseDto.CategoryId = await _categoryService.GetAsync(selectedProductproduct.CategoryId);

                _redisManager.Add(redisKey, productResponseDto, new TimeSpan(0, 5, 0));
                product = productResponseDto;
            }
            return product;
        }

        public async Task InsertAsync(ProductRequestDto productDto)
        {
            productDto.ValidCheckForInsertUpdate();

            var product = _mapper.Map<Product>(productDto);
            await _dataDao.InsertAsync(product);
        }

        public async Task UpdateAsync(string id, ProductRequestDto productDto)
        {
            productDto.ValidCheckForInsertUpdate();

            var product = _mapper.Map<Product>(productDto);
            await _dataDao.UpdateAsync(id, product);
        }

        public async Task<IEnumerable<ProductResponseDto>> FilterAsync(string name, string categoryId)
        {
            IEnumerable<Product> products;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(categoryId))
                products = await _dataDao.FilterAsync(p => p.Name.Contains(name) && p.CategoryId == categoryId);
            else if (!string.IsNullOrWhiteSpace(name))
                products = await _dataDao.FilterAsync(p => p.Name.Contains(name));
            else if (!string.IsNullOrWhiteSpace(categoryId))
                products = await _dataDao.FilterAsync(p => p.CategoryId == categoryId);
            else
                throw new ArgumentNullException();

            return await AddCategoriesOfProducts(products);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _dataDao.GetAllAsync();
            return await AddCategoriesOfProducts(products);
        }

        /// <summary>
        /// Fetch categories of products and add them to product models.
        /// </summary>
        /// <param name="products">Products withour categories.<see cref="IEnumerable{Product}"/></param>
        /// <returns>Products with categories.<see cref="IEnumerable{ProductResponseDto}"/></returns>
        private async Task<IEnumerable<ProductResponseDto>> AddCategoriesOfProducts(IEnumerable<Product> products)
        {
            var productResponnseDtoList = new List<ProductResponseDto>();
            foreach (var product in products)
            {
                var categoryResponseDto = await _categoryService.GetAsync(product.CategoryId);

                var productResponseDto = _mapper.Map<ProductResponseDto>(product);
                productResponseDto.CategoryId = categoryResponseDto;

                productResponnseDtoList.Add(productResponseDto);
            }

            return productResponnseDtoList;
        }
    }
}

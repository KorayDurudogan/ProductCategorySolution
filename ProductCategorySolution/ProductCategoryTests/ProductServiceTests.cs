using AutoMapper;
using FluentAssertions;
using Moq;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Infrastructure.Redis;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.Models;
using ProductCategory.Service.ProductServices;
using ProductCategory.Services.ProductServices;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProductCategoryTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IDataDao<Product>> _dataDao;

        private readonly Mock<IMapper> _mapper;

        private readonly Mock<ICategoryService> _categoryService;

        private readonly Mock<IRedisManager> _redisManager;

        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _dataDao = new Mock<IDataDao<Product>>();
            _mapper = new Mock<IMapper>();
            _categoryService = new Mock<ICategoryService>();
            _redisManager = new Mock<IRedisManager>();
            _productService = new ProductService(_dataDao.Object, _mapper.Object, _categoryService.Object, _redisManager.Object);
        }

        /// <summary>
        /// Covering null request case for DeleteAsync.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _productService.DeleteAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering successful case for DeleteAsync.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _productService.DeleteAsync("61563f90a0086ecc823c930b"));
            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering null request case for GetAsync.
        /// </summary>
        [Fact]
        public async Task GetAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _productService.GetAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null request case for InsertAsync.
        /// </summary>
        [Fact]
        public async Task InsertAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _productService.InsertAsync(It.IsAny<ProductRequestDto>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null name in request case for InsertAsync.
        /// </summary>
        [Fact]
        public async Task InsertAsync_ArgumentNullExceptionByName()
        {
            var exception = await Record.ExceptionAsync(() => _productService.InsertAsync(new ProductRequestDto
            {
                Description = "dummy_description"
            }));

            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering successful case for InsertAsync
        /// </summary>
        [Fact]
        public async Task InsertAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _productService.InsertAsync(new ProductRequestDto
            {
                Description = "dummy_description",
                Name = "asdasd",
                CategoryId = "61563eeca0086ecc823c9307",
                Currency = "TL",
                Price = 20.2M
            }));

            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering successful case for UpdateAsync
        /// </summary>
        [Fact]
        public async Task UpdateAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync("61563f90a0086ecc823c930b", new ProductRequestDto
            {
                Description = "dummy_description",
                Name = "asdasd",
                CategoryId = "61563eeca0086ecc823c9307",
                Currency = "TL",
                Price = 20.2M
            }));

            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering null id in request case for UpdateAsync
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ArgumentNullExceptionById()
        {
            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync(It.IsAny<string>(), new ProductRequestDto
            {
                Description = "dummy_description",
                Name = "asdasd"
            }));

            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null category model in request case for UpdateAsync
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ArgumentNullExceptionByModel()
        {
            var exception = await Record.ExceptionAsync(() => _productService.UpdateAsync("61563f90a0086ecc823c930b", It.IsAny<ProductRequestDto>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null id in request case for FilterAsync
        /// </summary>
        [Fact]
        public async Task FilterAsync_ArgumentNullExceptionByModel()
        {
            var exception = await Record.ExceptionAsync(() => _productService.FilterAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering success case for FilterAsync
        /// </summary>
        [Fact]
        public async Task FilterAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _productService.FilterAsync("61563f90a0086ecc823c930b"));
            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering success case for GetAllAsync
        /// </summary>
        [Fact]
        public async Task GetAllAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _productService.GetAllAsync());
            exception.Should().BeNull();
        }
    }
}

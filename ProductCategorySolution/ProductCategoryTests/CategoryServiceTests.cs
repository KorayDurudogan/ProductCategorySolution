using AutoMapper;
using FluentAssertions;
using Moq;
using ProductCategory.Infrastructure.DAOs;
using ProductCategory.Infrastructure.Models;
using ProductCategory.Service.CategoryServices;
using ProductCategory.Service.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProductCategoryTests
{
    public class CategoryServiceTests
    {
        private readonly Mock<IDataDao<Category>> _dataDao;

        private readonly Mock<IMapper> _mapper;

        private readonly ICategoryService _categoryService;

        public CategoryServiceTests()
        {
            _dataDao = new Mock<IDataDao<Category>>();
            _mapper = new Mock<IMapper>();
            _categoryService = new CategoryService(_dataDao.Object, _mapper.Object);
        }

        /// <summary>
        /// Covering null request case for DeleteAsync.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.DeleteAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering successful case for DeleteAsync.
        /// </summary>
        [Fact]
        public async Task DeleteAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.DeleteAsync("61563f90a0086ecc823c930b"));
            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering null request case for GetAsync.
        /// </summary>
        [Fact]
        public async Task GetAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.GetAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null request case for InsertAsync.
        /// </summary>
        [Fact]
        public async Task InsertAsync_ArgumentNullException()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.InsertAsync(It.IsAny<CategoryRequestDto>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null name in request case for InsertAsync.
        /// </summary>
        [Fact]
        public async Task InsertAsync_ArgumentNullExceptionByName()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.InsertAsync(new CategoryRequestDto
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
            var exception = await Record.ExceptionAsync(() => _categoryService.InsertAsync(new CategoryRequestDto
            {
                Description = "dummy_description",
                Name = "asdasd"
            }));

            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering successful case for UpdateAsync
        /// </summary>
        [Fact]
        public async Task UpdateAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.UpdateAsync("61563f90a0086ecc823c930b", new CategoryRequestDto
            {
                Description = "dummy_description",
                Name = "asdasd"
            }));

            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering null id in request case for UpdateAsync
        /// </summary>
        [Fact]
        public async Task UpdateAsync_ArgumentNullExceptionById()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.UpdateAsync(It.IsAny<string>(), new CategoryRequestDto
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
            var exception = await Record.ExceptionAsync(() => _categoryService.UpdateAsync("61563f90a0086ecc823c930b", It.IsAny<CategoryRequestDto>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering null id in request case for FilterAsync
        /// </summary>
        [Fact]
        public async Task FilterAsync_ArgumentNullExceptionByModel()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.FilterAsync(It.IsAny<string>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering success case for FilterAsync
        /// </summary>
        [Fact]
        public async Task FilterAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.FilterAsync("61563f90a0086ecc823c930b"));
            exception.Should().BeNull();
        }

        /// <summary>
        /// Covering success case for GetAllAsync
        /// </summary>
        [Fact]
        public async Task GetAllAsync_Success()
        {
            var exception = await Record.ExceptionAsync(() => _categoryService.GetAllAsync());
            exception.Should().BeNull();
        }
    }
}

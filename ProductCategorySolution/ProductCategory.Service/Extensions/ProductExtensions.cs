using ProductCategory.Service.Models;
using System;

namespace ProductCategory.Service.Extensions
{
    internal static class ProductExtensions
    {
        internal static void ValidCheckForInsertUpdate(this ProductRequestDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto?.Name) || productDto?.Price == default(decimal) || string.IsNullOrWhiteSpace(productDto?.Currency))
                throw new ArgumentNullException();
        }
    }
}

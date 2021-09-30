using MongoDB.Bson;

namespace ProductCategory.Service.Models
{
    /// <summary>
    /// A data transfer object for carrying product request/responses.
    /// </summary>
    public class ProductRequestDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryId { get; set; }

        public decimal Price { get; set; }

        public string Currency { get; set; }
    }
}

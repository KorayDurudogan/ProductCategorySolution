namespace ProductCategory.Service.Models
{
    public class ProductResponseDto : ProductRequestDto
    {
        public string Id { get; set; }

        public new CategoryResponseDto CategoryId { get; set; }
    }
}

using MongoDB.Bson;

namespace ProductCategory.Infrastructure.Models
{
    public class Category
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}

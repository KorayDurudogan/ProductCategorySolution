using MongoDB.Bson;
using MongoDB.Driver;
using ProductCategory.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductCategory.Infrastructure.DAOs
{
    public class ProductDao : IDataDao<Product>
    {
        private readonly IMongoCollection<Product> _products;

        public ProductDao(IMongoClient client)
        {
            var database = client.GetDatabase("products_db");
            _products = database.GetCollection<Product>("products");
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, ObjectId.Parse(id));
            await _products.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Product>> FilterAsync(Expression<Func<Product, bool>> query)
        {
            var filter = Builders<Product>.Filter.Where(query);
            return await _products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAll() => await _products.Find(_ => true).ToListAsync();

        public async Task<Product> GetAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
            return await _products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Product model) => await _products.InsertOneAsync(model);

        public async Task UpdateAsync(string id, Product model)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
            var update = Builders<Product>.Update
                .Set(p => p.CategoryId, model.CategoryId)
                .Set(p => p.Currency, model.Currency)
                .Set(p => p.Description, model.Description)
                .Set(p => p.Name, model.Name)
                .Set(p => p.Price, model.Price);

            await _products.UpdateOneAsync(filter, update);
        }
    }
}

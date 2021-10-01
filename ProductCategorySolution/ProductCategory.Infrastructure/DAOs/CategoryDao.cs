using MongoDB.Bson;
using MongoDB.Driver;
using ProductCategory.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductCategory.Infrastructure.DAOs
{
    public class CategoryDao : IDataDao<Category>
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryDao(IMongoClient client)
        {
            var database = client.GetDatabase("products_db");
            _categories = database.GetCollection<Category>("category");
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(p => p.Id, ObjectId.Parse(id));
            await _categories.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Category>> FilterAsync(Expression<Func<Category, bool>> query)
        {
            var filter = Builders<Category>.Filter.Where(query);
            return await _categories.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _categories.Find(_ => true).ToListAsync();

        public async Task<Category> GetAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
            return await _categories.Find(filter).FirstOrDefaultAsync();
        }

        public async Task InsertAsync(Category model) => await _categories.InsertOneAsync(model);

        public async Task UpdateAsync(string id, Category model)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, ObjectId.Parse(id));
            var update = Builders<Category>.Update
                .Set(p => p.Description, model.Description)
                .Set(p => p.Name, model.Name);

            await _categories.UpdateOneAsync(filter, update);
        }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApp
{
    /// <summary>
    /// Tầng Data – kết nối MongoDB, thực hiện CRUD và tìm kiếm.
    /// </summary>
    public class StudentRepository
    {
        private readonly IMongoCollection<Student> _collection;

        public StudentRepository(string connectionString, string databaseName = "StudentDB")
        {
            var client   = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _collection  = database.GetCollection<Student>("Students");
        }

        // ── CRUD ────────────────────────────────────────────────────────────────

        public async Task<List<Student>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Student?> GetByIdAsync(string id)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Student student) =>
            await _collection.InsertOneAsync(student);

        public async Task<bool> UpdateAsync(string id, Student updated)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
            var update = Builders<Student>.Update
                .Set(s => s.Name,    updated.Name)
                .Set(s => s.Email,   updated.Email)
                .Set(s => s.Address, updated.Address)
                .Set(s => s.Age,     updated.Age)
                .Set(s => s.Grade,   updated.Grade);

            var result = await _collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var filter = Builders<Student>.Filter.Eq(s => s.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        // ── Search ──────────────────────────────────────────────────────────────

        public async Task<List<Student>> SearchByNameAsync(string keyword)
        {
            var filter = Builders<Student>.Filter.Regex(
                s => s.Name, new BsonRegularExpression(keyword, "i"));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Student>> SearchByAddressAsync(string keyword)
        {
            var filter = Builders<Student>.Filter.Regex(
                s => s.Address, new BsonRegularExpression(keyword, "i"));
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<List<Student>> SearchByGradeAsync(string grade)
        {
            var filter = Builders<Student>.Filter.Regex(
                s => s.Grade, new BsonRegularExpression($"^{grade}$", "i"));
            return await _collection.Find(filter).ToListAsync();
        }
    }
}

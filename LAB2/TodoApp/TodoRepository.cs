using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public class TodoRepository
    {
        private readonly string _connectionString;

        public TodoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection ConnectDB()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            using var database = ConnectDB();
            var result = await database.QueryAsync<Todo>("SELECT * FROM Todos");
            return result.ToList();
        }

        public async Task<Todo> GetAsync(int id)
        {
            using var database = ConnectDB();
            var result = await database.QueryFirstAsync<Todo>(
                "SELECT * FROM Todos WHERE Id = @id", new { id });
            return result;
        }

        public async Task AddAsync(string title)
        {
            using var database = ConnectDB();
            string query = @"INSERT INTO Todos (Title, IsCompleted) VALUES (@Title, 0)";
            await database.ExecuteAsync(query, new { Title = title });
        }

        public async Task UpdateAsync(Todo todo)
        {
            using var database = ConnectDB();
            string query = @"UPDATE Todos SET Title = @Title, IsCompleted = @IsCompleted WHERE Id = @Id";
            await database.ExecuteAsync(query, todo);
        }

        public async Task DeleteAsync(int id)
        {
            using var database = ConnectDB();
            string query = @"DELETE FROM Todos WHERE Id = @Id";
            await database.ExecuteAsync(query, new { Id = id });
        }
    }
}

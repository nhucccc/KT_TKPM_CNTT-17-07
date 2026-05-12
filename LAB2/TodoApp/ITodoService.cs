using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public interface ITodoService
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo> GetAsync(int id);
        Task addASync(string title);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, string title);
        Task ToggleTodoAsync(int id);
    }
}

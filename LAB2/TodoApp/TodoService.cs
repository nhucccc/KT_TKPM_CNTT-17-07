using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAppV2
{
    public class TodoService : ITodoService
    {
        private readonly TodoRepository _repository;

        public TodoService(TodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Todo>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task addASync(string title) => await _repository.AddAsync(title);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task<Todo> GetAsync(int id) => await _repository.GetAsync(id);

        public async Task ToggleTodoAsync(int id)
        {
            Todo todo = await _repository.GetAsync(id);
            todo.IsCompleted = !todo.IsCompleted;
            await _repository.UpdateAsync(todo);
        }

        public async Task UpdateAsync(int id, string title)
        {
            Todo todo = await _repository.GetAsync(id);
            todo.Title = title;
            await _repository.UpdateAsync(todo);
        }
    }
}

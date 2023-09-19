using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ITodoRepository
    {
        Task Add(TodoItem item);
        Task<IEnumerable<TodoItem>> GetAll();
        Task<TodoItem> GetById(int id);
        Task Remove(int id);
        Task Update(TodoItem item);
    }
}
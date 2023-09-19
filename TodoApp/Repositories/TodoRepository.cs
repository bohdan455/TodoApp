using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItem>> GetAll()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetById(int id)
        {
            return await _dbContext.TodoItems.FindAsync(id) ?? throw new ArgumentException("Invalid todo item id");
        }

        public async Task Add(TodoItem item)
        {
            await _dbContext.TodoItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TodoItem item)
        {
            var todoItem = await GetById(item.Id);
            if (todoItem == null)
            {
                throw new ArgumentException("Invalid todo item id");
            }

            todoItem.Title = item.Title;
            todoItem.Description = item.Description;

            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            var todoItem = await GetById(id);
            if (todoItem == null)
            {
                throw new ArgumentException("Invalid todo item id");
            }

            _dbContext.TodoItems.Remove(todoItem);
            await _dbContext.SaveChangesAsync();
        }
    }
}

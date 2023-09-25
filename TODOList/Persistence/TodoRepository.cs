using Microsoft.EntityFrameworkCore;
using TODOList.Interfaces;
using TODOList.Models;

namespace TODOList.Persistence
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(int id)
        {
            var a = _context.TodoItems.ToList();
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddTodoItemAsync(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoItemAsync(int id)
        {
            var todoItem = await GetTodoItemAsync(id);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }
}

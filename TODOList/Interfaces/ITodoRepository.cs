using TODOList.Models;

namespace TODOList.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> GetTodoItemAsync(int id);
        Task AddTodoItemAsync(TodoItem todoItem);
        Task UpdateTodoItemAsync(TodoItem todoItem);
        Task DeleteTodoItemAsync(int id);
    }
}

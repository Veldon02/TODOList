using Microsoft.EntityFrameworkCore;
using TODOList.Models;
using TODOList.Persistence;

namespace TODOListTests
{
    public class TodoRepositoryTests
    {
        private readonly DbContextOptions<TodoDbContext> _options;
        private readonly TodoDbContext _context;
        private readonly TodoRepository _repository;

        public TodoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "TodoDB")
                .Options;
            _context = new TodoDbContext(_options);
            _repository = new TodoRepository(_context);
        }

        [Fact]
        public async Task GetTodoItemsAsync_ShouldReturnAllTodoItems()
        {
            // Arrange
            var todoItems = new List<TodoItem>
        {
            new TodoItem { Title = "TodoItem 1", IsCompleted = false },
            new TodoItem { Title = "TodoItem 2", IsCompleted = true },
            new TodoItem { Title = "TodoItem 3", IsCompleted = false }
        };
            await _context.TodoItems.AddRangeAsync(todoItems);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTodoItemsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetTodoItemAsync_ShouldReturnTodoItemWithGivenId()
        {
            // Arrange
            using var context = new TodoDbContext(_options);
            var repository = new TodoRepository(context);

            var todoItem = new TodoItem { Title = "Test TodoItem", IsCompleted = false };
            await context.TodoItems.AddAsync(todoItem);
            await context.SaveChangesAsync();

            // Act
            var retrievedTodoItem = await repository.GetTodoItemAsync(todoItem.Id);

            // Assert
            Assert.NotNull(retrievedTodoItem);
            Assert.Equal(todoItem.Id, retrievedTodoItem.Id);
            Assert.Equal(todoItem.Title, retrievedTodoItem.Title);
            Assert.False(retrievedTodoItem.IsCompleted);
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetTodoItemAsync_ShouldReturnNullWhenGivenInvalidId()
        {
            // Arrange
            using var context = new TodoDbContext(_options);
            var repository = new TodoRepository(context);

            // Act
            var retrievedTodoItem = await repository.GetTodoItemAsync(999);

            // Assert
            Assert.Null(retrievedTodoItem);
        }

        [Fact]
        public async Task AddTodoItemAsync_ShouldAddTodoItem()
        {
            // Arrange
            using var context = new TodoDbContext(_options);
            var repository = new TodoRepository(context);

            var todoItem = new TodoItem
            {
                Title = "Test TodoItem",
                Description = "This is a test TodoItem",
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            // Act
            await repository.AddTodoItemAsync(todoItem);

            // Assert
            var retrievedTodoItem = await context.TodoItems.FindAsync(todoItem.Id);
            Assert.NotNull(retrievedTodoItem);
            Assert.Equal(todoItem.Title, retrievedTodoItem.Title);
        }

        [Fact]
        public async Task UpdateTodoItemAsync_ShouldUpdateTodoItem()
        {
            // Arrange
            using var context = new TodoDbContext(_options);
            var repository = new TodoRepository(context);

            var todoItem = new TodoItem { Title = "Test TodoItem", IsCompleted = false };
            await context.TodoItems.AddAsync(todoItem);
            await context.SaveChangesAsync();

            // Act
            todoItem.Title = "Updated Test TodoItem";
            await repository.UpdateTodoItemAsync(todoItem);

            // Assert
            var retrievedTodoItem = await context.TodoItems.FindAsync(todoItem.Id);
            Assert.NotNull(retrievedTodoItem);
            Assert.Equal(todoItem.Title, retrievedTodoItem.Title);
        }

        [Fact]
        public async Task DeleteTodoItemAsync_ShouldDeleteTodoItem()
        {
            // Arrange
            using var context = new TodoDbContext(_options);
            var repository = new TodoRepository(context);

            var todoItem = new TodoItem { Title = "Test TodoItem", IsCompleted = false };
            await context.TodoItems.AddAsync(todoItem);
            await context.SaveChangesAsync();

            // Act
            await repository.DeleteTodoItemAsync(todoItem.Id);

            // Assert
            var retrievedTodoItem = await context.TodoItems.FindAsync(todoItem.Id);
            Assert.Null(retrievedTodoItem);
        }
    }
}

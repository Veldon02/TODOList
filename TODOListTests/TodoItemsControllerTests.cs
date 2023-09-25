using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TODOList.Controllers;
using TODOList.Interfaces;
using TODOList.Models;

namespace TODOListTests
{
    public class TodoItemsControllerTests
    {
        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly TodoItemsController _controller;

        public TodoItemsControllerTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _controller = new TodoItemsController(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetTodoItems_Returns_OkResult_With_TodoItems()
        {
            // Arrange
            var todoItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "TodoItem 1", IsCompleted = false },
            new TodoItem { Id = 2, Title = "TodoItem 2", IsCompleted = true }
        };
            _repositoryMock.Setup(repo => repo.GetTodoItemsAsync()).ReturnsAsync(todoItems);

            // Act
            var result = await _controller.GetTodoItems() as OkObjectResult;
            var todoItemsResult = result.Value as List<TodoItem>;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(todoItemsResult);
            Assert.Equal(todoItems.Count, todoItemsResult.Count);
        }

        [Fact]
        public async Task GetTodoItem_Returns_OkResult_With_TodoItem()
        {
            // Arrange
            var todoItem = new TodoItem { Id = 1, Title = "Test TodoItem", IsCompleted = false };
            _repositoryMock.Setup(repo => repo.GetTodoItemAsync(todoItem.Id)).ReturnsAsync(todoItem);

            // Act
            var result = await _controller.GetTodoItem(todoItem.Id) as OkObjectResult;
            var todoItemResult = result.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.NotNull(todoItemResult);
            Assert.Equal(todoItem.Id, todoItemResult.Id);
            Assert.Equal(todoItem.Title, todoItemResult.Title);
        }

        [Fact]
        public async Task GetTodoItem_Returns_NotFoundResult_When_Id_Does_Not_Exist()
        {
            // Arrange
            var todoItemId = 999;
            _repositoryMock.Setup(repo => repo.GetTodoItemAsync(todoItemId)).ReturnsAsync(null as TodoItem);

            // Act
            var result = await _controller.GetTodoItem(todoItemId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task CreateTodoItem_Returns_CreatedAtAction_With_TodoItem()
        {
            // Arrange
            var todoItem = new TodoItem { Id = 1, Title = "Test TodoItem", IsCompleted = false };

            // Act
            var result = await _controller.CreateTodoItem(todoItem) as CreatedAtActionResult;
            var todoItemResult = result.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            Assert.Equal("GetTodoItem", result.ActionName);
            Assert.Equal(todoItem.Id, result.RouteValues["id"]);
            Assert.NotNull(todoItemResult);
            Assert.Equal(todoItem.Id, todoItemResult.Id);
            Assert.Equal(todoItem.Title, todoItemResult.Title);
        }

        [Fact]
        public async Task CreateTodoItem_Returns_BadRequest_When_Model_State_Invalid()
        {
            // Arrange
            var todoItem = new TodoItem { Id = 1, Title = null, IsCompleted = false };
            _controller.ModelState.AddModelError("Title", "Title is required");

            // Act
            var result = await _controller.CreateTodoItem(todoItem) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.NotNull(result.Value);
            Assert.IsType<SerializableError>(result.Value);
            var errors = result.Value as SerializableError;
            Assert.NotNull(errors);
            Assert.Equal(1, errors.Count);
            Assert.True(errors.ContainsKey("Title"));
            Assert.Equal(new[] { "Title is required" }, (string[])errors["Title"]);
        }

        [Fact]
        public async Task UpdateTodoItem_Returns_NoContentResult()
        {
            // Arrange
            var todoItemId = 1;
            var todoItem = new TodoItem { Id = todoItemId, Title = "Test TodoItem", IsCompleted = false };

            // Act
            var result = await _controller.UpdateTodoItem(todoItemId, todoItem) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task UpdateTodoItem_Returns_BadRequest_When_Id_Mismatch()
        {
            // Arrange
            var todoItemId = 1;
            var todoItem = new TodoItem { Id = 2, Title = "Test TodoItem", IsCompleted = false };

            // Act
            var result = await _controller.UpdateTodoItem(todoItemId, todoItem) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task UpdateTodoItem_Returns_NotFound_When_TodoItem_Not_Exists()
        {
            // Arrange
            var todoItemId = 1;
            var todoItem = new TodoItem { Id = todoItemId, Title = "Test TodoItem", IsCompleted = false };
            _repositoryMock.Setup(repo => repo.UpdateTodoItemAsync(todoItem)).ThrowsAsync(new DbUpdateConcurrencyException());
            _repositoryMock.Setup(repo => repo.GetTodoItemAsync(todoItemId)).ReturnsAsync(null as TodoItem);

            // Act
            var result = await _controller.UpdateTodoItem(todoItemId, todoItem) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task DeleteTodoItem_Returns_NoContentResult()
        {
            // Arrange
            var todoItemId = 1;

            // Act
            var result = await _controller.DeleteTodoItem(todoItemId) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }
    }
}

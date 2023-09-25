using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TODOList.Interfaces;
using TODOList.Models;

namespace TODOList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _repository;

        public TodoItemsController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            var todoItems = await _repository.GetTodoItemsAsync();
            return Ok(todoItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem(int id)
        {
            var todoItem = await _repository.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoItem(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddTodoItemAsync(todoItem);

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateTodoItemAsync(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TodoItemExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _repository.DeleteTodoItemAsync(id);
            return NoContent();
        }

        private async Task<bool> TodoItemExistsAsync(int id)
        {
            return await _repository.GetTodoItemAsync(id) != null;
        }
    }
}

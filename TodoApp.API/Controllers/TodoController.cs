using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Data;
using TodoApp.API.Models;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoDbContext.Todos.ToListAsync();

            return Ok(todos);
        }
        [HttpPost]
        public async Task<IActionResult> AddTodo(Todo todo)
        {
            todo.Id = Guid.NewGuid();

            await _todoDbContext.Todos.AddAsync(todo);
            await _todoDbContext.SaveChangesAsync();
            return Ok(todo);
        }
        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute]  Guid id , Todo todoupdteRequest)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);
            if (todo == null)
            
                return NotFound();
                todo.IsCompleted = todoupdteRequest.IsCompleted;
                todo.CompletedDate = DateTime.Now;
                await _todoDbContext.SaveChangesAsync();
            
                 return Ok(todo);


        }





    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiSample.Data;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (!_context.TodoItems.Any())
            {
                _context.TodoItems.Add(new TodoItem() {Id = 1, Content = "111"});
                _context.TodoItems.Add(new TodoItem() {Id = 2, Content = "222"});
                _context.TodoItems.Add(new TodoItem() {Id = 3, Content = "333"});
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return Ok("ok");
        }

        [HttpPut]
        public IActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            var todo = _context.TodoItems.SingleOrDefault(t => t.Id == id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.Content = "333";
            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return Ok("ok");
        }
    }
}
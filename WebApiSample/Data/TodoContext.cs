using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;

namespace WebApiSample.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using TODOList.Models;

public class TodoDbContext : DbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the TodoItem entity
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Description).HasColumnName("description").HasMaxLength(250);
            entity.Property(e => e.IsCompleted).HasColumnName("is_completed");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime");

            entity.HasData(
                new TodoItem { Id = 1, Title = "Buy groceries", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Do laundry", IsCompleted = false },
                new TodoItem { Id = 3, Title = "Finish project", IsCompleted = false }
            );
        });
    }
}
using System.ComponentModel.DataAnnotations;

namespace TODOList.Models
{
    public class TodoItem   
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }
    }
}

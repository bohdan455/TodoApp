using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class TodoItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace NotesContactsMvc.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required, Display(Name = "Назва замітки"), StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required, Display(Name = "Текст замітки")]
        public string Content { get; set; } = string.Empty;

        [Display(Name = "Дата створення")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Теги (через кому)")]
        public string? Tags { get; set; }

        public IEnumerable<string> GetTagList() =>
            (Tags ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(t => t);
    }
}

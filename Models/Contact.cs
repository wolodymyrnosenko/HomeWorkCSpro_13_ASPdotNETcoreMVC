using System.ComponentModel.DataAnnotations;

namespace NotesContactsMvc.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, Display(Name = "Ім'я контакту"), StringLength(80)]
        public string Name { get; set; } = string.Empty;

        [Required, Phone, Display(Name = "Мобільний телефон")]
        public string Mobile { get; set; } = string.Empty;

        [Phone, Display(Name = "Альтернативний мобільний")]
        public string? AltMobile { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Короткий опис"), StringLength(300)]
        public string? About { get; set; }
    }
}

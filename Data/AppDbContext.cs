using Microsoft.EntityFrameworkCore;
using NotesContactsMvc.Models;

namespace NotesContactsMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<Contact> Contacts => Set<Contact>();
    }
}

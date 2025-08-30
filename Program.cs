using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NotesContactsMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("notesContactsDb"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Index}/{id?}");

// Seed demo data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Notes.Any() && !db.Contacts.Any())
    {
        db.Notes.AddRange(
            new NotesContactsMvc.Models.Note { Title = "Перша замітка", Content = "Текст замітки...", Tags = "mvc,aspnet", CreatedAt = DateTime.Now },
            new NotesContactsMvc.Models.Note { Title = "Ідеї", Content = "1) Вивчити EF Core\n2) Написати тести", Tags = "ideas,todo", CreatedAt = DateTime.Now.AddMinutes(-30) }
        );
        db.Contacts.AddRange(
            new NotesContactsMvc.Models.Contact { Name = "Олена", Mobile = "+380501112233", AltMobile = "+380671112233", Email = "olena@example.com", About = "Колега з відділу" },
            new NotesContactsMvc.Models.Contact { Name = "Іван", Mobile = "+380931234567", AltMobile = null, Email = "ivan@example.com", About = "Друг дитинства" }
        );
        db.SaveChanges();
    }
}

app.Run();

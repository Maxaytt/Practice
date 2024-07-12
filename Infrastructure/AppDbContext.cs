using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Film> Films { get; set; }

    }
}

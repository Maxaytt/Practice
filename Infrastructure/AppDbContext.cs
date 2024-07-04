using System.Formats.Asn1;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Answer> Answers {get;set;}
        public virtual  DbSet<Question> Questions{get;set;}
    }
}

using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ParserContext : DbContext
    {
        public ParserContext(DbContextOptions<ParserContext> options) : base(options)
        {
        }

        public DbSet<Sentence> Sentences { get; set; }
    }
}

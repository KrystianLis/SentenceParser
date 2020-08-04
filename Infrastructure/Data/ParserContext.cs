using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

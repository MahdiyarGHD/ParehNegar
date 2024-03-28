using Microsoft.EntityFrameworkCore;
using ParehNegar.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Contexts
{
    public sealed class ParehNegarContext : DbContext
    {
        private DatabaseBuilder _builder;
        public ParehNegarContext(DatabaseBuilder builder) 
        {
            _builder = builder;
        }
        public DbSet<PersonEntity> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_builder != null)
            {
                _builder.OnConfiguring(optionsBuilder);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

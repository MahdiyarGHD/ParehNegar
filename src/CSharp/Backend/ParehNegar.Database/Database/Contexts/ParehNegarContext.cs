using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Database.Contexts
{
    public sealed class AppTaxContext : DbContext
    {
        public AppTaxContext(DbContextOptions builder) : base(builder)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}

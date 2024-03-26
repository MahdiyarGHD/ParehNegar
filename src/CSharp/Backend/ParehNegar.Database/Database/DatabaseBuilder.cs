using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParehNegar.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database
{
    public class DatabaseBuilder
    {
        private readonly IConfiguration _configuration;
        public DatabaseBuilder(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var entity = GetEntity();
            if (entity.IsSqlServer())
                optionsBuilder.UseSqlServer(entity.ConnectionString);
            else if (entity.IsInMemory())
                optionsBuilder.UseInMemoryDatabase("AppTax");
        }

        public DatabaseConfig GetEntity()
        {
            return GetDatabases()?.Where((DatabaseConfig x) => !string.IsNullOrEmpty(x.Name)).FirstOrDefault((DatabaseConfig x) => x.Name.Equals("Entity", StringComparison.OrdinalIgnoreCase));
        }

        public virtual List<DatabaseConfig> GetDatabases()
        {
            return _configuration?.GetSection("Databases")?.Get<List<DatabaseConfig>>();
        }
    }
}

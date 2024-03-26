using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Models
{
    /* by EasyMicroservice */
    public class DatabaseConfig
    {
        public string Name { get; set; }

        public string ProviderName { get; set; }

        public string ConnectionString { get; set; }

        public bool IsSqlServer()
        {
            if (string.IsNullOrEmpty(ProviderName))
                return false;

            return ProviderName.Equals("SqlServer", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsInMemory()
        {
            if (string.IsNullOrEmpty(ProviderName))
                return false;

            return ProviderName.Equals("InMemory", StringComparison.OrdinalIgnoreCase);
        }
    }
}

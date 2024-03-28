using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Entities
{
    public class PersonEntity : IIdSchema<long>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}

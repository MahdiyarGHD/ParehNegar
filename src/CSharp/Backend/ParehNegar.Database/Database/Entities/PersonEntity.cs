using ParehNegar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Database.Entities
{
    public class PersonEntity : FullAbilityIdSchema<long>
    {
        public string? Name { get; set; }
        public long Age { get; set; }
    }
}

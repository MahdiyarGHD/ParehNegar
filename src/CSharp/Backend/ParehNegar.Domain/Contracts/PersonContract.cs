using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts
{
    public class PersonContract : FullAbilityIdSchema<long>
    {
        public string? Name { get; set; }
        public long Age { get; set; }
    }
}

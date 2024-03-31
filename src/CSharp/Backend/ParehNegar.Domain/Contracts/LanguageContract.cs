using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts
{
    public class LanguageContract : FullAbilityIdSchema<long>
    {
        public string Name { get; set; }

        public ICollection<ContentContract> Contents { get; set; }
    }
}

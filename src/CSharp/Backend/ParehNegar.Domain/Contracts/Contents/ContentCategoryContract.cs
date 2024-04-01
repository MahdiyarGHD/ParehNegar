using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents
{
    public class ContentCategoryContract : FullAbilityIdSchema<long>
    {
        public string? Key { get; set; }

        public List<ContentContract> Contents { get; set; }
    }
}

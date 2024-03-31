using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts
{
    public class ContentCategoryContract
    {
        public string? Key { get; set; }

        public ICollection<ContentContract> Contents { get; set; }
    }
}

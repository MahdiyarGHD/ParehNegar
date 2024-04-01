using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents
{
    public class ContentCategoryResponseContract : IIdSchema<long>
    {
        public long Id { get; set; }
        public string Key { get; set; }
        public List<ContentResponseContract> Contents { get; set; }
    }
}

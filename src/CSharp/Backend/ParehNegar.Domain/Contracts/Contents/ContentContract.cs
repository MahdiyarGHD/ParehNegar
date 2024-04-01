using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents
{
    public class ContentContract : FullAbilityIdSchema<long>
    {
        public string Data { get; set; }

        public long LanguageId { get; set; }
        public long CategoryId { get; set; }
        public LanguageContract Language { get; set; }
        public ContentCategoryContract Category { get; set; }
    }
}

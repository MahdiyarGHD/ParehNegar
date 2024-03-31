using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts
{
    public class ContentContract 
    {
        public string Data { get; set; }

        public long LanguageId { get; set; }
        public LanguageContract Language { get; set; }
        public long CategoryId { get; set; }
        public ContentCategoryContract Category { get; set; }
    }
}

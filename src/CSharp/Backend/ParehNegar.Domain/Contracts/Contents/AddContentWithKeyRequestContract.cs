using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents
{
    public class AddContentWithKeyRequestContract
    {
        public string Key { get; set; }
        public List<LanguageDataContract> LanguageData { get; set; }
    }
}

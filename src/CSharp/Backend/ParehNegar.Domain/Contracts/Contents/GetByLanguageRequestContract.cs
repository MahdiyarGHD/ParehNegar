using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents;

public class GetByLanguageRequestContract
{
    public string Language { get; set; }
    public string Key { get; set; }
}
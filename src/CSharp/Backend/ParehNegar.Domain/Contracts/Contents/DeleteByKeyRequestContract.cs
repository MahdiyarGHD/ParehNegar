using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents;

public class DeleteByKeyRequestContract
{
    public string Key { get; set; }
}
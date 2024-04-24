using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts.Contents;

public class ContentResponseContract : IIdSchema<long>
{
    public long Id { get; set; }
    public string Data { get; set; }

    public long LanguageId { get; set; }
    public long CategoryId { get; set; }
    public LanguageResponseContract Language { get; set; }
    public ContentCategoryResponseContract Category { get; set; }
}
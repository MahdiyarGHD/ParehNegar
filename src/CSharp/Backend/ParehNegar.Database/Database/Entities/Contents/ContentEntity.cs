using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Database.Entities.Contents;

public class ContentEntity : FullAbilityIdSchema<long>
{
    public string Data { get; set; }

    public long LanguageId { get; set; }
    public LanguageEntity Language { get; set; }
    public long CategoryId { get; set; }
    public ContentCategoryEntity Category { get; set; }
}
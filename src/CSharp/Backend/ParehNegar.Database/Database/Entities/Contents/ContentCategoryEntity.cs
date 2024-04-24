using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Database.Entities.Contents;

public class ContentCategoryEntity : FullAbilityIdSchema<long>
{
    public string Key { get; set; }

    public ICollection<ContentEntity> Contents { get; set; }
}
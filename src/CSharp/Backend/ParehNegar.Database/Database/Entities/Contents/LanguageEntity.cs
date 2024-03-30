using ParehNegar.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Database.Database.Entities.Contents
{
    public class LanguageEntity : FullAbilityIdSchema<long>
    {
        public string Name { get; set; }

        public ICollection<ContentEntity> Contents { get; set; }
    }
}

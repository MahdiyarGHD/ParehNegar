﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.BaseModels;

public class FullAbilitySchema : ISoftDeleteSchema, IDateTimeSchema
{
    public DateTime CreationDateTime { get; set; }

    public DateTime? ModificationDateTime { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedDateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.BaseModels;

public interface IIdSchema<TId> where TId : new()
{
    public TId Id { get; set; }
}

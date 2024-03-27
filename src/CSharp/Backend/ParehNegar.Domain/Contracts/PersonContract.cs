using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Domain.Contracts
{
    public class PersonContract
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}

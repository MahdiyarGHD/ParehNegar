using ParehNegar.Logics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Logics
{
    public class UnitOfWork(IServiceProvider service) : BaseUnitOfWork(service), IUnitOfWork
    {
        public ContentHelper GetContentHelper()
        {
            return GetService<ContentHelper>(); 
        }

    }
}

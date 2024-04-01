using ParehNegar.Logics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Logics
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        public ContentHelper GetContentHelper();
    }
}

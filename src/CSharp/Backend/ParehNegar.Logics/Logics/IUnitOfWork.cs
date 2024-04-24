using ParehNegar.Logics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Managers;

namespace ParehNegar.Logics.Logics;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ContentHelper GetContentHelper();
    public ClaimManager GetClaimManager();
    public IJWTHelper GetJWTHelper();
}
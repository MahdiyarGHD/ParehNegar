using ParehNegar.Logics.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParehNegar.Logics.Interfaces;
using ParehNegar.Logics.Managers;

namespace ParehNegar.Logics.Logics;

public class UnitOfWork(IServiceProvider service) : BaseUnitOfWork(service), IUnitOfWork
{
    public ContentHelper GetContentHelper()
    {
        return GetService<ContentHelper>(); 
    }

    public ClaimManager GetClaimManager()
    {
        return GetService<ClaimManager>();
    }

    public IJWTHelper GetJWTHelper()
    {
        return GetService<IJWTHelper>();
    }
}
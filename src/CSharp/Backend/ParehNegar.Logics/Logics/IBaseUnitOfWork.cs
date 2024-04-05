using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Serialization.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParehNegar.Domain.BaseModels;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Helpers;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Logics
{
    public interface IBaseUnitOfWork
    {
        public ValueTask DisposeAsync();
        public IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract> GetContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>()
            where TResponseContract : class
            where TId : new()
            where TEntity : class, IIdSchema<TId>;
        public IContractLogic<long, TEntity, TContract, TContract, TContract> GetLongContractLogic<TEntity, TContract>()
            where TContract : class
            where TEntity : class, IIdSchema<long>;
        public Logic<TEntity, TId> GetLogic<TEntity, TId>() where TId : new() where TEntity : class, IIdSchema<TId>;

        IConfiguration GetConfiguration();
        string GetValue(string key);
        public ContentLanguageHelper GetContentLanguageHelper();
        IMapperProvider GetMapper();
        DbContext GetDbContext();
        ITextSerializationProvider GetTextSerialization();
    }
}

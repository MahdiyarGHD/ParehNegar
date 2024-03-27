using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Serialization.Interfaces;
using Microsoft.Extensions.Configuration;
using ParehNegar.Logics.DatabaseLogics;
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
        IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract> GetContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>()
  where TResponseContract : class
  where TEntity : class;
        Logic<TEntity> GetLogic<TEntity>()
          where TEntity : class;
        IConfiguration GetConfiguration();
        string GetValue(string key);
        IMapperProvider GetMapper();
        ITextSerializationProvider GetTextSerialization();
    }
}

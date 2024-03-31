using AutoMapper;
using EasyMicroservices.Mapper.CompileTimeMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Mapper.SerializerMapper.Providers;
using EasyMicroservices.Serialization.Interfaces;
using EasyMicroservices.Serialization.Newtonsoft.Json.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParehNegar.Database;
using ParehNegar.Domain.BaseModels;
using ParehNegar.Logics.DatabaseLogics;
using ParehNegar.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Logics
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        public IServiceProvider ServiceProvider { get; protected set; }
        public static Type MapperTypeAssembly { get; set; }

        public BaseUnitOfWork(IServiceProvider service)
        {
            service.ThrowIfNull(nameof(service));
            ServiceProvider = service;
        }

        List<object> Disposables { get; set; } = new List<object>();

        T AddDisposable<T>(T data)
        {
            Disposables.Add(data);
            return data;
        }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public virtual async ValueTask DisposeAsync()
        {
            InternalSyncDispose();
            await InternalDispose();
            Disposables.Clear();
            ServiceProvider = null;
        }
        async Task InternalDispose()
        {
            foreach (var item in Disposables)
            {
                if (item is IAsyncDisposable disposable)
                {
                    await disposable.DisposeAsync();
                }
            }
        }

        void InternalSyncDispose()
        {
            foreach (var item in Disposables)
            {
                if (item is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        public virtual IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract> GetContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>()
            where TResponseContract : class
            where TId : new()
            where TEntity : class, IIdSchema<TId>
        {
            return GetInternalContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>();
        }

        IContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract> GetInternalContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>()
            where TResponseContract : class
            where TId : new()

            where TEntity : class, IIdSchema<TId>
        {
            return AddDisposable(new ContractLogic<TId, TEntity, TCreateRequestContract, TUpdateRequestContract, TResponseContract>(AddDisposable(GetService<DbContext>()), AddDisposable(GetService<IMapperProvider>())));
        }

        public virtual Logic<TEntity, TId> GetLogic<TEntity, TId>()
            where TId : new()
            where TEntity : class, IIdSchema<TId>
        {
            return GetInternaltLogic<TEntity, TId>();
        }

        Logic<TEntity, TId> GetInternaltLogic<TEntity, TId>()
            where TId : new()
            where TEntity : class, IIdSchema<TId>
        {
            return AddDisposable(new Logic<TEntity, TId>(GetService<DbContext>()));
        }


        public IConfiguration GetConfiguration()
        {
            return ServiceProvider.GetService<IConfiguration>();
        }

        public virtual string GetValue(string key)
        {
            IConfiguration config = GetService<IConfiguration>();
            return (key.IsNullOrEmpty() || config == null) ? null : config.GetValue<string>(key);
        }

        public virtual IMapperProvider GetMapper()
        {
            var mapper = new CompileTimeMapperProvider(new SerializerMapperProvider(new NewtonsoftJsonProvider(new Newtonsoft.Json.JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            })));
            if (MapperTypeAssembly != null)
            {
                foreach (var type in MapperTypeAssembly.Assembly.GetTypes())
                {
                    if (typeof(IMapper).IsAssignableFrom(type))
                    {
                        var instance = Activator.CreateInstance(type, mapper);
                        var returnTypes = type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.ReturnType != typeof(object) && x.Name == "Map").Select(x => x.ReturnType).ToArray();
                        mapper.AddMapper(returnTypes[0], returnTypes[1], (EasyMicroservices.Mapper.CompileTimeMapper.Interfaces.IMapper)instance);
                    }
                }
            }
            return mapper;
        }

        public ITextSerializationProvider GetTextSerialization()
        {
            return ServiceProvider.GetService<ITextSerializationProvider>();
        }
    }
}

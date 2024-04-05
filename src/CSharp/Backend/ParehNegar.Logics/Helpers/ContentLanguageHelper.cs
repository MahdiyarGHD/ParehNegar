using EasyMicroservices.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using ParehNegar.Domain.Attributes;
using ParehNegar.Domain.Contracts.Contents;
using ParehNegar.Logics.Logics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParehNegar.Logics.Helpers
{
    public class ContentLanguageHelper(IUnitOfWork unitOfWork)
    {
        private readonly ContentHelper _contentHelper = unitOfWork.GetContentHelper();


        public async Task ResolveContentAllLanguage(IEnumerable items)
        {
            if (items == null)
                return;
            List<Task> tasks = new List<Task>();
            foreach (var item in items)
            {
                tasks.Add(ResolveContentAllLanguage(item));
            }
            await Task.WhenAll(tasks);
        }

        public async Task ResolveContentAllLanguage(object contract)
        {
            if (contract == null)
                return;
            var identifierAttr = contract.GetType().GetCustomAttribute(typeof(ContentIdentifierAttribute)) as ContentIdentifierAttribute;
            if (identifierAttr.Identifier.IsNullOrEmpty())
                return;
            
            object id = contract.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public).GetValue(contract);
            if (id is null)
                return;

            string uniqueIdentity = identifierAttr.Identifier;
            foreach (var property in contract.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (property.GetCustomAttribute<ContentLanguageAttribute>() != null)
                {
                    var instance = Activator.CreateInstance(property.PropertyType);
                    if (instance is IList list)
                    {
                        var genericType = property.PropertyType.GetGenericArguments()[0];
                        var contents = await _contentHelper.GetAllByKey(new GetAllByKeyRequestContract
                        {
                            Key = string.Join("-", uniqueIdentity, id, GetPropertyName(property))
                        });
                        if (contents.IsSuccess)
                        {
                            foreach (var item in contents.Result)
                            {
                                var itemInstance = Activator.CreateInstance(genericType);
                                var languageProperty = itemInstance.GetType().GetProperty(nameof(LanguageDataContract.Language), BindingFlags.Public | BindingFlags.Instance);
                                if (languageProperty == null)
                                    throw new Exception($"Property {nameof(LanguageDataContract.Language)} not found in type {itemInstance.GetType()}");
                                var dataProperty = itemInstance.GetType().GetProperty(nameof(LanguageDataContract.Data), BindingFlags.Public | BindingFlags.Instance);
                                if (dataProperty == null)
                                    throw new Exception($"Property {nameof(LanguageDataContract.Data)} not found in type {itemInstance.GetType()}");
                                languageProperty.SetValue(itemInstance, item.Language.Name);
                                dataProperty.SetValue(itemInstance, item.Data);
                                list.Add(itemInstance);
                            }
                        }
                    }
                    property.SetValue(contract, instance);
                }
            }
        }

        public async Task ResolveContentLanguage(IEnumerable items, string language)
        {
            if (items == null)
                return;
            List<Task> tasks = new List<Task>();
            foreach (var item in items)
            {
                tasks.Add(ResolveContentLanguage(item, language, new HashSet<object>()));
            }
            await Task.WhenAll(tasks);
        }

        public Task ResolveContentLanguage(object contract, string language)
        {
            return ResolveContentLanguage(contract, language, new HashSet<object>());
        }

        async Task ResolveContentLanguage(object contract, string language, HashSet<object> mappedItems)
        {
            if (contract.Equals(default) || mappedItems.Contains(contract))
                return;
            var type = contract.GetType();
            mappedItems.Add(contract);
            if (IsClass(type) && typeof(IEnumerable).IsAssignableFrom(type))
            {
                foreach (var item in (IEnumerable)contract)
                {
                    await ResolveContentLanguage(item, language, mappedItems);
                }
            }
            else
            {
                var identifierAttr = contract.GetType().GetCustomAttribute(typeof(ContentIdentifierAttribute)) as ContentIdentifierAttribute;
                if (identifierAttr is not null && identifierAttr.Identifier.IsNullOrEmpty())
                    return;
            
                object id = contract.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public).GetValue(contract);
                if (id is null)
                    return;

                foreach (var property in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (property.GetCustomAttribute<ContentLanguageAttribute>() != null)
                    {
                        var contentResult = await _contentHelper.GetByLanguage(new GetByLanguageRequestContract()
                        {
                            Key = string.Join("-", identifierAttr.Identifier, id, GetPropertyName(property)),
                            Language = language
                        });
                        if (contentResult.IsSuccess)
                            property.SetValue(contract, contentResult.Result.Data);
                    }
                    else if (IsClass(property.PropertyType) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        var items = property.GetValue(contract);
                        if (items == null)
                            continue;
                        foreach (var item in (IEnumerable)items)
                        {
                            await ResolveContentLanguage(item, language, mappedItems);
                        }
                    }
                    else if (IsClass(property.PropertyType))
                    {
                        var value = property.GetValue(contract);
                        if (value != null)
                            await ResolveContentLanguage(value, language, mappedItems);
                    }
                }
            }
        }

        bool IsClass(Type type)
        {
            return type.GetTypeInfo().IsClass && typeof(string) != type && typeof(char[]) != type;
        }

        public Task<MessageContract<ContentCategoryResponseContract>> AddToContentLanguage(object item)
        {
            return SaveToContentLanguage(item, AddToContent);
        }

        public Task<List<Task<MessageContract<ContentCategoryResponseContract>>>> AddToContentLanguage(IEnumerable items)
        {
            List<Task<MessageContract<ContentCategoryResponseContract>>> tasks = new List<Task<MessageContract<ContentCategoryResponseContract>>>();
            foreach (var item in items)
            {
                tasks.Add(AddToContentLanguage(item));
            }
            return Task.FromResult(tasks);
        }

        public Task<MessageContract> UpdateToContentLanguage(object item)
        {
            return SaveToContentLanguageUpdate(item, UpdateToContent);
        }

        public async Task UpdateToContentLanguage(IEnumerable items)
        {
            List<Task> tasks = new List<Task>();
            foreach (var item in items)
            {
                tasks.Add(UpdateToContentLanguage(item));
            }
            await Task.WhenAll(tasks);
        }

        async Task<MessageContract<ContentCategoryResponseContract>> SaveToContentLanguage(object item, Func<(string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)[], Task<MessageContract<ContentCategoryResponseContract>>> saveData)
        {
            if (item.Equals(default))
                return new MessageContract<ContentCategoryResponseContract>()
                {
                    IsSuccess = true,
                };
            var identifierAttr = item.GetType().GetCustomAttribute(typeof(ContentIdentifierAttribute)) as ContentIdentifierAttribute;
            if (identifierAttr.Identifier.IsNullOrEmpty())
                return (FailedReasonType.Empty, "Identifier is not initialized correctly.");
            
            object id = item.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public).GetValue(item);
            if (id is null)
                return (FailedReasonType.Empty, "Id cannot be empty.");

            string uniqueIdentity = $"{identifierAttr.Identifier}-{id}";
            var request = new List<(string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)>();
            foreach (var property in item.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (TryGetPropertyName(property, out string propertyName))
                {
                    if (property.GetValue(item) is IEnumerable items)
                    {
                        request.Add((uniqueIdentity, propertyName, Map(items)));
                    }
                }
            }
            var result = await saveData(request.ToArray());
            if (!result.IsSuccess)
            {
                LogContract(result.Error);
                return result;
            }

            return new MessageContract<ContentCategoryResponseContract>()
            {
                IsSuccess = true,
            };
        }

        void LogContract(ErrorContract contract)
        {
            Console.WriteLine($"Content Error Message: {contract.Message}");
            Console.WriteLine($"Content Error EndUserMessage: {contract.EndUserMessage}");
            Console.WriteLine($"Content Error Details: {contract.Details}");
            Console.WriteLine($"Content Error ProjectName: {contract.ServiceDetails?.ProjectName}");
            Console.WriteLine($"Content Error MethodName: {contract.ServiceDetails?.MethodName}");
        }

        async Task<MessageContract> SaveToContentLanguageUpdate(object item, Func<(string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)[], Task<MessageContract>> saveData)
        {
            if (item.Equals(default))
                return new MessageContract()
                {
                    IsSuccess = true,
                };
            var identifierAttr = item.GetType().GetCustomAttribute(typeof(ContentIdentifierAttribute)) as ContentIdentifierAttribute;
            if (identifierAttr.Identifier.IsNullOrEmpty())
                return (FailedReasonType.Empty, "Identifier is not initialized correctly.");

            object id = item.GetType().GetProperty("Id", BindingFlags.Instance | BindingFlags.Public).GetValue(item);
            if (id is null)
                return (FailedReasonType.Empty, "Id cannot be empty.");

            string uniqueIdentity = $"{identifierAttr.Identifier}-{id}";
            var request = new List<(string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)>();
            foreach (var property in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (TryGetPropertyName(property, out string propertyName))
                {
                    if (property.GetValue(item) is IEnumerable items)
                    {
                        request.Add((uniqueIdentity, propertyName, Map(items)));
                    }
                }
            }
            var result = await saveData(request.ToArray());
            if (!result.IsSuccess)
            {
                LogContract(result.Error);
                return result;
            }
            return new MessageContract()
            {
                IsSuccess = true,
            };
        }

        bool TryGetPropertyName(PropertyInfo property, out string propertyName)
        {
            var contentLanguageAttribute = property.GetCustomAttribute<ContentLanguageAttribute>();
            propertyName = GetPropertyName(property);
            return contentLanguageAttribute != null;
        }

        string GetPropertyName(PropertyInfo property)
        {
            var contentLanguageAttribute = property.GetCustomAttribute<ContentLanguageAttribute>();
            if (contentLanguageAttribute != null)
                return contentLanguageAttribute.PropertyName ?? property.Name;
            else
                return property.Name;
        }

        IEnumerable<LanguageDataContract> Map(IEnumerable objects)
        {
            if (objects != null)
            {
                foreach (var item in objects)
                {
                    var languageProperty = item.GetType().GetProperty(nameof(LanguageDataContract.Language), BindingFlags.Public | BindingFlags.Instance);
                    if (languageProperty == null)
                        throw new Exception($"Property {nameof(LanguageDataContract.Language)} not found in type {item.GetType()}");
                    var dataProperty = item.GetType().GetProperty(nameof(LanguageDataContract.Data), BindingFlags.Public | BindingFlags.Instance);
                    if (dataProperty == null)
                        throw new Exception($"Property {nameof(LanguageDataContract.Data)} not found in type {item.GetType()}");
                    yield return new LanguageDataContract()
                    {
                        Language = languageProperty.GetValue(item) as string,
                        Data = dataProperty.GetValue(item) as string,
                    };
                }
            }
        }

        async Task<MessageContract<ContentCategoryResponseContract>> AddToContent(string uniqueIdentity, string name, IEnumerable<LanguageDataContract> languages)
        {
            var addNames = await _contentHelper.AddContentWithKey(new AddContentWithKeyRequestContract
            {
                Key = $"{uniqueIdentity}-{name}",
                LanguageData = languages.Select(o => new LanguageDataContract
                {
                    Data = o.Data,
                    Language = o.Language
                }).ToList(),
            });

            return addNames;
        }


        async Task<MessageContract<ContentCategoryResponseContract>> AddToContent(params (string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)[] items)
        {
            MessageContract<ContentCategoryResponseContract> result = default;
            foreach (var item in items)
            {
                result = await AddToContent(item.UniqueIdentity, item.Name, item.Languages);
                if (!result.IsSuccess)
                {
                    LogContract(result.Error);
                    return result;
                }
            }
            return result;
        }

        async Task<MessageContract> UpdateToContent(string uniqueIdentity, string name, IEnumerable<LanguageDataContract> languages)
        {
            var addNames = await _contentHelper.UpdateContentWithKey(new AddContentWithKeyRequestContract
            {
                Key = $"{uniqueIdentity}-{name}",
                LanguageData = languages.Select(o => new LanguageDataContract
                {
                    Data = o.Data,
                    Language = o.Language
                }).ToList(),
            });

            return addNames;
        }

        async Task<MessageContract> UpdateToContent(params (string UniqueIdentity, string Name, IEnumerable<LanguageDataContract> Languages)[] items)
        {
            MessageContract result = default;
            foreach (var item in items)
            {
                result = await UpdateToContent(item.UniqueIdentity, item.Name, item.Languages);
                if (!result.IsSuccess)
                {
                    LogContract(result.Error);
                    return result;
                }
            }
            return result;
        }
    }
}

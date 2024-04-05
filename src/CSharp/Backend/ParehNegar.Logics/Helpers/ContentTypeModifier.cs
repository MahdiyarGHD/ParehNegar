using ParehNegar.Domain.Attributes;
using ParehNegar.Domain.Contracts.Contents;
using System.Reflection.Emit;
using System.Reflection;
using ParehNegar.Domain.Contracts;

public static class ContentTypeModifier
{
    public static Type ModifyMultilingualRequestType(Type originalType)
    {
        List<PropertyInfo> propertiesToModify = originalType.GetProperties()
            .Where(prop => Attribute.IsDefined(prop, typeof(ContentLanguageAttribute)))
            .ToList();

        // Create a new type with modified properties
        var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("ModifiedAssembly"), AssemblyBuilderAccess.Run);
        var moduleBuilder = assemblyBuilder.DefineDynamicModule("ModifiedModule");
        var typeBuilder = moduleBuilder.DefineType(originalType.Name + "Modified", TypeAttributes.Public | TypeAttributes.Class);

        // Copy all attributes from original type to modified type
        foreach (var attribute in originalType.GetCustomAttributesData())
        {
            var constructorArgs = attribute.ConstructorArguments.Select(arg => arg.Value).ToArray();
            var namedArgInfos = attribute.NamedArguments
                .Select(arg =>
                {
                    MemberInfo member;
                    if (arg.MemberInfo is FieldInfo fieldInfo)
                        member = fieldInfo;
                    else if (arg.MemberInfo is PropertyInfo propertyInfo)
                        member = propertyInfo;
                    else
                        throw new ArgumentException("Invalid member info type");
                    return new { Member = member, arg.TypedValue.Value };
                })
                .ToArray();

            var namedArgs = namedArgInfos.Select(arg => arg.Member as FieldInfo).ToArray();
            var values = namedArgInfos.Select(arg => arg.Value).ToArray();

            var attributeBuilder = new CustomAttributeBuilder(attribute.Constructor, constructorArgs, namedArgs, values);
            typeBuilder.SetCustomAttribute(attributeBuilder);
        }

        // Define properties for unmodified properties
        foreach (var propertyInfo in originalType.GetProperties())
        {
            if (!propertiesToModify.Contains(propertyInfo))
            {
                var propertyBuilder = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.None, propertyInfo.PropertyType, null);

                // Copy attributes from original property to new property
                foreach (var attribute in propertyInfo.GetCustomAttributesData())
                {
                    var constructorArgs = attribute.ConstructorArguments.Select(arg => arg.Value).ToArray();
                    var namedArgInfos = attribute.NamedArguments
                        .Select(arg =>
                        {
                            MemberInfo member;
                            if (arg.MemberInfo is FieldInfo fieldInfo)
                                member = fieldInfo;
                            else if (arg.MemberInfo is PropertyInfo propertyInfo)
                                member = propertyInfo;
                            else
                                throw new ArgumentException("Invalid member info type");
                            return new { Member = member, arg.TypedValue.Value };
                        })
                        .ToArray();

                    var namedArgs = namedArgInfos.Select(arg => arg.Member as FieldInfo).ToArray();
                    var values = namedArgInfos.Select(arg => arg.Value).ToArray();

                    var attributeBuilder = new CustomAttributeBuilder(attribute.Constructor, constructorArgs, namedArgs, values);
                    typeBuilder.SetCustomAttribute(attributeBuilder);
                }

                // Define getter method for unmodified properties
                var getterMethodBuilder = typeBuilder.DefineMethod("get_" + propertyInfo.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyInfo.PropertyType, Type.EmptyTypes);
                var getterIL = getterMethodBuilder.GetILGenerator();
                getterIL.Emit(OpCodes.Ldarg_0);
                getterIL.Emit(OpCodes.Call, propertyInfo.GetGetMethod());
                getterIL.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(getterMethodBuilder);

                // Define setter method for unmodified properties
                var setterMethodBuilder = typeBuilder.DefineMethod("set_" + propertyInfo.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { propertyInfo.PropertyType });
                var setterIL = setterMethodBuilder.GetILGenerator();
                setterIL.Emit(OpCodes.Ldarg_0);
                setterIL.Emit(OpCodes.Ldarg_1);
                setterIL.Emit(OpCodes.Call, propertyInfo.GetSetMethod());
                setterIL.Emit(OpCodes.Ret);
                propertyBuilder.SetSetMethod(setterMethodBuilder);
            }
        }

        // Define properties with IEnumerable<LanguageDataContract> type for modified properties
        foreach (var propertyInfo in propertiesToModify)
        {
            var propertyBuilder = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.None, typeof(IEnumerable<LanguageDataContract>), null);

            // Copy attributes from original property to modified property
            foreach (var attribute in propertyInfo.GetCustomAttributesData())
            {
                var constructorArgs = attribute.ConstructorArguments.Select(arg => arg.Value).ToArray();
                var namedArgInfos = attribute.NamedArguments
                    .Select(arg =>
                    {
                        MemberInfo member;
                        if (arg.MemberInfo is FieldInfo fieldInfo)
                            member = fieldInfo;
                        else if (arg.MemberInfo is PropertyInfo propertyInfo)
                            member = propertyInfo;
                        else
                            throw new ArgumentException("Invalid member info type");
                        return new { Member = member, arg.TypedValue.Value };
                    })
                    .ToArray();

                var namedArgs = namedArgInfos.Select(arg => arg.Member as FieldInfo).ToArray();
                var values = namedArgInfos.Select(arg => arg.Value).ToArray();

                var attributeBuilder = new CustomAttributeBuilder(attribute.Constructor, constructorArgs, namedArgs, values);
                propertyBuilder.SetCustomAttribute(attributeBuilder);
            }

            // Define getter method for modified properties
            var getterMethodBuilder = typeBuilder.DefineMethod("get_" + propertyInfo.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, typeof(IEnumerable<LanguageDataContract>), Type.EmptyTypes);
            var getterIL = getterMethodBuilder.GetILGenerator();
            getterIL.Emit(OpCodes.Ldarg_0);
            getterIL.Emit(OpCodes.Call, propertyInfo.GetGetMethod());
            getterIL.Emit(OpCodes.Ret);
            propertyBuilder.SetGetMethod(getterMethodBuilder);

            // Define setter method for modified properties
            var setterMethodBuilder = typeBuilder.DefineMethod("set_" + propertyInfo.Name, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { typeof(IEnumerable<LanguageDataContract>) });
            var setterIL = setterMethodBuilder.GetILGenerator();
            setterIL.Emit(OpCodes.Ldarg_0);
            setterIL.Emit(OpCodes.Ldarg_1);
            setterIL.Emit(OpCodes.Call, propertyInfo.GetSetMethod());
            setterIL.Emit(OpCodes.Ret);
            propertyBuilder.SetSetMethod(setterMethodBuilder);
        }

        // Create the modified type
        var modifiedType = typeBuilder.CreateType();

        return modifiedType;
    }
}

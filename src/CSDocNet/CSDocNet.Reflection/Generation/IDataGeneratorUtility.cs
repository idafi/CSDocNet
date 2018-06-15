using System;
using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IDataGeneratorUtility
    {
        string GenerateName(MemberInfo info);
        IReadOnlyList<MemberRef> GenerateAttributes(MemberInfo memberInfo);

        Type GetRootElementType(Type type);
        MemberRef MakeMemberRef(MemberInfo member);

        IReadOnlyList<TypeParam> GetTypeParams(IReadOnlyList<Type> typeArgs);
        ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes);
        IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam);

        bool IsReadOnly(ICustomAttributeProvider member);
    }
}
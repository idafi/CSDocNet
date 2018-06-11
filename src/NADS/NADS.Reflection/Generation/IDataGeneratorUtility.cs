using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IDataGeneratorUtility
    {
        string GenerateName(MemberInfo info);
        IReadOnlyList<MemberRef> GenerateAttributes(MemberInfo memberInfo);

        Type GetRootElementType(Type type);
        MemberRef MakeMemberRef(MemberInfo member);

        ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes);
        IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam);

        bool IsReadOnly(ICustomAttributeProvider member);
    }
}
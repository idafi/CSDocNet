using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IDocGeneratorUtility
    {
        IReadOnlyList<MemberRef> GenerateAttributes(MemberInfo memberInfo);

        MemberRef MakeMemberRef(MemberInfo member);
        ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes);
        IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam);
    }
}
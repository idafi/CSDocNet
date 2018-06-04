using System;
using System.Collections.Generic;
using System.Reflection;

namespace NADS.Reflection
{
    public interface IDocGeneratorUtility
    {
        MemberRef MakeMemberRef(MemberInfo member);
        
        ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes);
        IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam);
    }
}
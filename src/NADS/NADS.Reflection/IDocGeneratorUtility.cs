using System;
using System.Collections.Generic;
using System.Reflection;

namespace NADS.Reflection
{
    public interface IDocGeneratorUtility
    {
        MemberRef MakeMemberRef(Type type);
        MemberRef MakeMemberRef(EventInfo eventInfo);
        MemberRef MakeMemberRef(FieldInfo fieldInfo);
        MemberRef MakeMemberRef(PropertyInfo property);
        MemberRef MakeMemberRef(ConstructorInfo ctorInfo);
        MemberRef MakeMemberRef(MethodInfo methodInfo);
        
        ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes);
        IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam);
    }
}
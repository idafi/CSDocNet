using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSDocNet.Reflection
{
    public interface INameGenerator
    {
        string GenerateMemberName(MemberInfo member);

        string GenerateTypeName(Type type);
        string GenerateEventName(EventInfo e);
        string GenerateFieldName(FieldInfo field);
        string GeneratePropertyName(PropertyInfo property);
        string GenerateMethodName(MethodBase method);
        string GenerateOperatorName(string opMethodName);
    }
}
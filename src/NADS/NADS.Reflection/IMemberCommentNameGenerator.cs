using System;
using System.Reflection;

namespace NADS.Reflection
{
    public interface IMemberCommentNameGenerator
    {
        string GenerateTypeName(Type type);
        string GenerateFieldName(FieldInfo field);
        string GeneratePropertyName(PropertyInfo property);
        string GenerateMethodName(MethodInfo method);
        string GenerateMethodName(ConstructorInfo ctor);
        string GenerateEventName(EventInfo ev);
    }
}
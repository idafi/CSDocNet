using System;
using System.Reflection;

namespace NADS.Reflection
{
    public interface ICommentIDGenerator
    {
        string GenerateTypeID(Type type);
        string GenerateFieldID(FieldInfo field);
        string GeneratePropertyID(PropertyInfo property);
        string GenerateMethodID(MethodInfo method);
        string GenerateMethodID(ConstructorInfo ctor);
        string GenerateEventID(EventInfo ev);
    }
}
using System;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
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
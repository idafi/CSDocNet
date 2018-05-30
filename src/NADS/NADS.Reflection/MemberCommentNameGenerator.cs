using System;
using System.Reflection;
using NADS.Debug;

namespace NADS.Reflection
{
    public class MemberCommentNameGenerator : IMemberCommentNameGenerator
    {
        public string GenerateTypeName(Type type)
        {
            Check.Ref(type);

            return $"T:{FormatTypeName(type)}";
        }

        public string GenerateFieldName(FieldInfo field)
        {
            Check.Ref(field);
            
            string type = FormatTypeName(field.DeclaringType);
            return $"F:{type}.{field.Name}";
        }

        public string GeneratePropertyName(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public string GenerateMethodName(MethodInfo method)
        {
            throw new NotImplementedException();
        }

        public string GenerateMethodName(ConstructorInfo ctor)
        {
            throw new NotImplementedException();
        }

        public string GenerateEventName(EventInfo ev)
        {
            throw new NotImplementedException();
        }

        string FormatTypeName(Type type)
        {
            Assert.Ref(type);

            string name = type.ToString();
            name = name.Replace('+', '.');      // convert nested type syntax
            if(type.ContainsGenericParameters)  // strip typeparam names, if present
            { name = name.Substring(0, name.IndexOf('[')); }

            return name;
        }
    }
}
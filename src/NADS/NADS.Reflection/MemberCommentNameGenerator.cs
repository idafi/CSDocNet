using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NADS.Debug;

namespace NADS.Reflection
{
    public class MemberCommentNameGenerator : IMemberCommentNameGenerator
    {
        public string GenerateTypeName(Type type)
        {
            Check.Ref(type);

            return $"T:{FormatTypeName(type, false)}";
        }

        public string GenerateFieldName(FieldInfo field)
        {
            Check.Ref(field);
            
            string type = FormatTypeName(field.DeclaringType, false);
            return $"F:{type}.{field.Name}";
        }

        public string GeneratePropertyName(PropertyInfo property)
        {
            throw new NotImplementedException();
        }

        public string GenerateMethodName(MethodInfo method)
        {
            Check.Ref(method);

            int typeParamCt = method.GetGenericArguments().Length;

            string typeName = FormatTypeName(method.DeclaringType, false);
            string typeParams = (typeParamCt > 0) ? $"``{typeParamCt}" : "";
            string paramList = FormatParameterList(method.GetParameters());
            
            return $"M:{typeName}.{method.Name}{typeParams}{paramList}";
        }

        public string GenerateMethodName(ConstructorInfo ctor)
        {
            throw new NotImplementedException();
        }

        public string GenerateEventName(EventInfo ev)
        {
            throw new NotImplementedException();
        }

        string FormatTypeName(Type type, bool listTypeParams)
        {
            Assert.Ref(type);

            string name = type.ToString();
            name = name.Replace('+', '.');

            int typeParamList = name.IndexOf('[');
            if(typeParamList > -1)
            { name = name.Substring(0, typeParamList); }

            if(listTypeParams)
            {
                Span<Type> typeParams = type.GetGenericArguments();
                MatchCollection tpCtMatches = Regex.Matches(name, @"\`(\d+)");
                
                for(int i = 0; i < tpCtMatches.Count; i++)
                {
                    Match match = tpCtMatches[tpCtMatches.Count - 1 - i];
                    int typeParamCount = int.Parse(match.Groups[1].Value);

                    Span<Type> usedParams = typeParams.Slice(typeParams.Length - typeParamCount);
                    string list = '{' + FormatTypeList(usedParams) + '}';

                    name = name.Remove(match.Index, match.Length);
                    name = name.Insert(match.Index, list);

                    typeParams = typeParams.Slice(0, typeParams.Length - typeParamCount);
                }
            }

            return name;
        }

        string FormatParameterList(ParameterInfo[] parameters)
        {
            if(parameters != null && parameters.Length > 0)
            {
                Type[] types =
                    (from ParameterInfo param in parameters
                    select param.ParameterType).ToArray();

                return $"({FormatTypeList(types)})";
            }

            return "";
        }

        string FormatTypeList(Span<Type> types)
        {
            if(types == null || types.Length <= 0)
            { return ""; }

            string[] paramNames = new string[types.Length];
            for(int i = 0; i < types.Length; i++)
            {
                ref string name = ref paramNames[i];
                Type type = types[i];

                if(type.IsByRef)
                {
                    name = "@";
                    type = type.GetElementType();
                }

                while(type.IsArray)
                {
                    int dimCount = type.GetArrayRank();

                    if(dimCount > 1)
                    {
                        string[] prefixes = new string[dimCount];
                        for(int d = 0; d < dimCount; d++)
                        { prefixes[d] = "0:"; }

                        name = $"[{string.Join(",", prefixes)}]" + name;
                    }
                    else
                    { name = "[]" + name; }

                    type = type.GetElementType();
                }

                if(type.IsGenericParameter)
                {
                    name = type.GenericParameterPosition + name;

                    if(type.DeclaringMethod != null)
                    { name = "``" + name; }
                    else
                    { name = '`' + name; }
                }
                else
                { name = FormatTypeName(type, true) + name; }
            }

            return string.Join(",", paramNames);
        }
    }
}
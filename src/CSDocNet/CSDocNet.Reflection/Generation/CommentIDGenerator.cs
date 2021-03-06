using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using CSDocNet.Debug;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class CommentIDGenerator : ICommentIDGenerator
    {
        static readonly Regex typeParamCountRegex = new Regex(@"\`(\d+)");

        public string GenerateMemberID(Type type)
        {
            Check.Ref(type);

            return $"T:{FormatTypeName(type)}";
        }

        public string GenerateMemberID(FieldInfo field)
        {
            Check.Ref(field);
            
            string type = FormatTypeName(field.DeclaringType);
            return $"F:{type}.{field.Name}";
        }

        public string GenerateMemberID(PropertyInfo property)
        {
            Check.Ref(property);

            string typeName = FormatTypeName(property.DeclaringType);
            string paramList = FormatParameterList(property.GetIndexParameters());

            return $"P:{typeName}.{property.Name}{paramList}";
        }

        public string GenerateMemberID(MethodBase method)
        {
            switch(method)
            {
                case MethodInfo m:
                    return GenerateMemberID(m);
                case ConstructorInfo c:
                    return GenerateMemberID(c);
                default:
                    throw new NotSupportedException($"Method '{method.Name}' has unsupported type '{method.GetType()}'");
            }
        }

        public string GenerateMemberID(MethodInfo method)
        {
            Check.Ref(method);

            int typeParamCt = method.GetGenericArguments().Length;

            string typeName = FormatTypeName(method.DeclaringType);
            string typeParams = (typeParamCt > 0) ? $"``{typeParamCt}" : "";
            string paramList = FormatParameterList(method.GetParameters());
            string returnType = (method.IsSpecialName
            && (method.Name == "op_Explicit" || method.Name == "op_Implicit"))
                ? $"~{FormatTypeName(method.ReturnType)}"
                : "";
            
            return $"M:{typeName}.{method.Name}{typeParams}{paramList}{returnType}";
        }

        public string GenerateMemberID(ConstructorInfo ctor)
        {
            Check.Ref(ctor);

            string typeName = FormatTypeName(ctor.DeclaringType);
            string paramList = FormatParameterList(ctor.GetParameters());

            return $"M:{typeName}.#ctor{paramList}";
        }

        public string GenerateMemberID(EventInfo ev)
        {
            Check.Ref(ev);
            
            string type = FormatTypeName(ev.DeclaringType);
            return $"E:{type}.{ev.Name}";
        }

        string FormatTypeName(Type type, bool listTypeParams = false)
        {
            Assert.Ref(type);
            
            string name = type.ToString();
            name = name.Replace('+', '.');
            
            int tpListPos = name.IndexOf('[');
            if(tpListPos > -1)
            { name = name.Substring(0, tpListPos); }
            
            if(listTypeParams)
            {
                ReadOnlySpan<Type> typeParams = type.GetGenericArguments();
                MatchCollection tpCtMatches = typeParamCountRegex.Matches(name);
                
                for(int i = 0; i < tpCtMatches.Count; i++)
                {
                    // we insert typeparam lists in reverse, so that we don't offset match start indices
                    Match match = tpCtMatches[tpCtMatches.Count - 1 - i];
                    int typeParamCount = int.Parse(match.Groups[1].Value);
                    int usedParamsStart = typeParams.Length - typeParamCount;
                    
                    ReadOnlySpan<Type> usedParams = typeParams.Slice(usedParamsStart);
                    string list = FormatTypeParamList(usedParams);
                    
                    name = name.Remove(match.Index, match.Length);
                    name = name.Insert(match.Index, list);
                    
                    typeParams = typeParams.Slice(0, typeParamCount);
                }
            }
            
            return name;
        }

        string FormatTypeParamList(ReadOnlySpan<Type> typeParams)
        {
            string[] names = new string[typeParams.Length];
            for(int i = 0; i < names.Length; i++)
            { names[i] = FormatParamName(typeParams[i]); }

            string list = string.Join(",", names);
            return '{' + list + '}';
        }

        string FormatParameterList(IReadOnlyList<ParameterInfo> parameters)
        {
            Assert.Ref(parameters);

            if(parameters.Count < 1)
            { return ""; }
            
            string[] names = new string[parameters.Count];
            for(int i = 0; i < names.Length; i++)
            { names[i] = FormatParamName(parameters[i].ParameterType); }

            string list = string.Join(",", names);
            return $"({list})";
        }

        string FormatParamName(Type type)
        {
            Assert.Ref(type);

            string name = "";
            
            // handle ref/out/in
            if(type.IsByRef)
            {
                name += "@";
                type = type.GetElementType();
            }
            
            // handle arrays
            while(type.IsArray)
            {
                int dimCount = type.GetArrayRank();
                
                // handle multidimensional arrays
                if(dimCount > 1)
                {
                    string[] dimMarkers = new string[dimCount];
                    for(int d = 0; d < dimCount; d++)
                    { dimMarkers[d] = "0:"; }
                
                    name = $"[{string.Join(",", dimMarkers)}]" + name;
                }
                else
                { name = "[]" + name; }

                type = type.GetElementType();
            }
            
            // handle generics
            if(type.IsGenericParameter)
            {
                int pos = type.GenericParameterPosition;
                string prefix = (type.DeclaringMethod != null)
                    ? $"``{pos}"
                    : $"`{pos}";
                
                name = prefix + name;
            }
            else
            { name = FormatTypeName(type, true) + name; }
            
            return name;
        }
    }
}
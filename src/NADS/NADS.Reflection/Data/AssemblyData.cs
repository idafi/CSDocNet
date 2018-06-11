using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct AssemblyData
    {
        public readonly IReadOnlyDictionary<int, ClassData> Classes;
        public readonly IReadOnlyDictionary<int, ClassData> Structs;
        public readonly IReadOnlyDictionary<int, ClassData> Interfaces;
        public readonly IReadOnlyDictionary<int, EnumData> Enums;
        public readonly IReadOnlyDictionary<int, MethodData> Delegates;
        
        public readonly IReadOnlyDictionary<int, MemberData> Events;
        public readonly IReadOnlyDictionary<int, FieldData> Fields;
        public readonly IReadOnlyDictionary<int, PropertyData> Properties;
        public readonly IReadOnlyDictionary<int, MethodData> Constructors;
        public readonly IReadOnlyDictionary<int, OperatorData> Operators;
        public readonly IReadOnlyDictionary<int, MethodData> Methods;	
        
        public AssemblyData(
            IReadOnlyDictionary<int, ClassData> classes,
            IReadOnlyDictionary<int, ClassData> structs,
            IReadOnlyDictionary<int, ClassData> interfaces,
            IReadOnlyDictionary<int, EnumData> enums,
            IReadOnlyDictionary<int, MethodData> delegates,
            
            IReadOnlyDictionary<int, MemberData> events,
            IReadOnlyDictionary<int, FieldData> fields,
            IReadOnlyDictionary<int, PropertyData> properties,
            IReadOnlyDictionary<int, MethodData> constructors,
            IReadOnlyDictionary<int, OperatorData> operators,
            IReadOnlyDictionary<int, MethodData> methods
        )
        {
            Classes = classes ?? Empty<int, ClassData>.Dict;
            Structs = classes ?? Empty<int, ClassData>.Dict;
            Interfaces = interfaces ?? Empty<int, ClassData>.Dict;
            Enums = enums ?? Empty<int, EnumData>.Dict;
            Delegates = delegates ?? Empty<int, MethodData>.Dict;
            
            Events = events ?? Empty<int, MemberData>.Dict;
            Fields = fields ?? Empty<int, FieldData>.Dict;
            Properties = properties ?? Empty<int, PropertyData>.Dict;
            Constructors = constructors ?? Empty<int, MethodData>.Dict;
            Operators = operators ?? Empty<int, OperatorData>.Dict;
            Methods = methods ?? Empty<int, MethodData>.Dict;
        }
    }
}
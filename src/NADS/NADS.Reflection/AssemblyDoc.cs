using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection
{
    public readonly struct AssemblyDoc
    {
        public readonly IReadOnlyDictionary<string, ClassDoc> Classes;
        public readonly IReadOnlyDictionary<string, ClassDoc> Structs;
        public readonly IReadOnlyDictionary<string, InterfaceDoc> Interfaces;
        public readonly IReadOnlyDictionary<string, EnumDoc> Enums;
        public readonly IReadOnlyDictionary<string, MethodDoc> Delegates;
        
        public readonly IReadOnlyDictionary<string, MemberDoc> Events;
        public readonly IReadOnlyDictionary<string, FieldDoc> Fields;
        public readonly IReadOnlyDictionary<string, PropertyDoc> Properties;
        public readonly IReadOnlyDictionary<string, MethodDoc> Constructors;
        public readonly IReadOnlyDictionary<string, OperatorDoc> Operators;
        public readonly IReadOnlyDictionary<string, MethodDoc> Methods;	
        
        public AssemblyDoc(
            IReadOnlyDictionary<string, ClassDoc> classes,
            IReadOnlyDictionary<string, ClassDoc> structs,
            IReadOnlyDictionary<string, InterfaceDoc> interfaces,
            IReadOnlyDictionary<string, EnumDoc> enums,
            IReadOnlyDictionary<string, MethodDoc> delegates,
            
            IReadOnlyDictionary<string, MemberDoc> events,
            IReadOnlyDictionary<string, FieldDoc> fields,
            IReadOnlyDictionary<string, PropertyDoc> properties,
            IReadOnlyDictionary<string, MethodDoc> constructors,
            IReadOnlyDictionary<string, OperatorDoc> operators,
            IReadOnlyDictionary<string, MethodDoc> methods
        )
        {
            Classes = classes ?? Empty<string, ClassDoc>.EmptyDict;
            Structs = classes ?? Empty<string, ClassDoc>.EmptyDict;
            Interfaces = interfaces ?? Empty<string, InterfaceDoc>.EmptyDict;
            Enums = enums ?? Empty<string, EnumDoc>.EmptyDict;
            Delegates = delegates ?? Empty<string, MethodDoc>.EmptyDict;
            
            Events = events ?? Empty<string, MemberDoc>.EmptyDict;
            Fields = fields ?? Empty<string, FieldDoc>.EmptyDict;
            Properties = properties ?? Empty<string, PropertyDoc>.EmptyDict;
            Constructors = constructors ?? Empty<string, MethodDoc>.EmptyDict;
            Operators = operators ?? Empty<string, OperatorDoc>.EmptyDict;
            Methods = methods ?? Empty<string, MethodDoc>.EmptyDict;
        }
    }
}
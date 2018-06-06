using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
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
            Classes = classes ?? Empty<string, ClassDoc>.Dict;
            Structs = classes ?? Empty<string, ClassDoc>.Dict;
            Interfaces = interfaces ?? Empty<string, InterfaceDoc>.Dict;
            Enums = enums ?? Empty<string, EnumDoc>.Dict;
            Delegates = delegates ?? Empty<string, MethodDoc>.Dict;
            
            Events = events ?? Empty<string, MemberDoc>.Dict;
            Fields = fields ?? Empty<string, FieldDoc>.Dict;
            Properties = properties ?? Empty<string, PropertyDoc>.Dict;
            Constructors = constructors ?? Empty<string, MethodDoc>.Dict;
            Operators = operators ?? Empty<string, OperatorDoc>.Dict;
            Methods = methods ?? Empty<string, MethodDoc>.Dict;
        }
    }
}
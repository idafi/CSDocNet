using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct AssemblyDoc
    {
        public readonly IReadOnlyDictionary<int, ClassDoc> Classes;
        public readonly IReadOnlyDictionary<int, ClassDoc> Structs;
        public readonly IReadOnlyDictionary<int, ClassDoc> Interfaces;
        public readonly IReadOnlyDictionary<int, EnumDoc> Enums;
        public readonly IReadOnlyDictionary<int, MethodDoc> Delegates;
        
        public readonly IReadOnlyDictionary<int, MemberDoc> Events;
        public readonly IReadOnlyDictionary<int, FieldDoc> Fields;
        public readonly IReadOnlyDictionary<int, PropertyDoc> Properties;
        public readonly IReadOnlyDictionary<int, MethodDoc> Constructors;
        public readonly IReadOnlyDictionary<int, OperatorDoc> Operators;
        public readonly IReadOnlyDictionary<int, MethodDoc> Methods;	
        
        public AssemblyDoc(
            IReadOnlyDictionary<int, ClassDoc> classes,
            IReadOnlyDictionary<int, ClassDoc> structs,
            IReadOnlyDictionary<int, ClassDoc> interfaces,
            IReadOnlyDictionary<int, EnumDoc> enums,
            IReadOnlyDictionary<int, MethodDoc> delegates,
            
            IReadOnlyDictionary<int, MemberDoc> events,
            IReadOnlyDictionary<int, FieldDoc> fields,
            IReadOnlyDictionary<int, PropertyDoc> properties,
            IReadOnlyDictionary<int, MethodDoc> constructors,
            IReadOnlyDictionary<int, OperatorDoc> operators,
            IReadOnlyDictionary<int, MethodDoc> methods
        )
        {
            Classes = classes ?? Empty<int, ClassDoc>.Dict;
            Structs = classes ?? Empty<int, ClassDoc>.Dict;
            Interfaces = interfaces ?? Empty<int, ClassDoc>.Dict;
            Enums = enums ?? Empty<int, EnumDoc>.Dict;
            Delegates = delegates ?? Empty<int, MethodDoc>.Dict;
            
            Events = events ?? Empty<int, MemberDoc>.Dict;
            Fields = fields ?? Empty<int, FieldDoc>.Dict;
            Properties = properties ?? Empty<int, PropertyDoc>.Dict;
            Constructors = constructors ?? Empty<int, MethodDoc>.Dict;
            Operators = operators ?? Empty<int, OperatorDoc>.Dict;
            Methods = methods ?? Empty<int, MethodDoc>.Dict;
        }
    }
}
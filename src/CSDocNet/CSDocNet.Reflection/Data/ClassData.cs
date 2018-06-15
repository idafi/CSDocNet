using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class ClassData
    {
        public readonly MemberData Member;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public readonly MemberRef Inherits;
        public readonly IReadOnlyList<MemberRef> Implements;

        public readonly IReadOnlyList<MemberRef> Events;
        public readonly IReadOnlyList<MemberRef> Fields;
        public readonly IReadOnlyList<MemberRef> Properties;
        public readonly IReadOnlyList<MemberRef> Constructors;
        public readonly IReadOnlyList<MemberRef> Operators;
        public readonly IReadOnlyList<MemberRef> Methods;

        public ClassData(MemberData member,
            IReadOnlyList<TypeParam> typeParams,
            MemberRef inherits,
            IReadOnlyList<MemberRef> implements,
            IReadOnlyList<MemberRef> events,
            IReadOnlyList<MemberRef> fields,
            IReadOnlyList<MemberRef> properties,
            IReadOnlyList<MemberRef> constructors,
            IReadOnlyList<MemberRef> operators,
            IReadOnlyList<MemberRef> methods
        )
        {
            Member = member;
            TypeParams = typeParams;
            
            Inherits = inherits;
            Implements = implements ?? Empty<MemberRef>.List;

            Events = events ?? Empty<MemberRef>.List;
            Fields = fields ?? Empty<MemberRef>.List;
            Properties = properties ?? Empty<MemberRef>.List;
            Constructors = constructors ?? Empty<MemberRef>.List;
            Operators = operators ?? Empty<MemberRef>.List;
            Methods = methods ?? Empty<MemberRef>.List;
        }
    }
}
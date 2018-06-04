using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct ClassDoc
    {
        public readonly MemberDoc Member;

        public readonly MemberRef Inherits;
        public readonly IReadOnlyList<MemberRef> Implements;

        public readonly IReadOnlyList<MemberRef> Events;
        public readonly IReadOnlyList<MemberRef> Fields;
        public readonly IReadOnlyList<MemberRef> Properties;
        public readonly IReadOnlyList<MemberRef> Constructors;
        public readonly IReadOnlyList<MemberRef> Operators;
        public readonly IReadOnlyList<MemberRef> Methods;

        public ClassDoc(in MemberDoc member,
            in MemberRef inherits,
            IReadOnlyList<MemberRef> implements,
            IReadOnlyList<MemberRef> events,
            IReadOnlyList<MemberRef> fields,
            IReadOnlyList<MemberRef> properties,
            IReadOnlyList<MemberRef> constructors,
            IReadOnlyList<MemberRef> operators,
            IReadOnlyList<MemberRef> methods)
        {
            Member = member;
            
            Inherits = inherits;
            Implements = implements ?? Empty<MemberRef>.EmptyList;

            Events = events ?? Empty<MemberRef>.EmptyList;
            Fields = fields ?? Empty<MemberRef>.EmptyList;
            Properties = properties ?? Empty<MemberRef>.EmptyList;
            Constructors = constructors ?? Empty<MemberRef>.EmptyList;
            Operators = operators ?? Empty<MemberRef>.EmptyList;
            Methods = methods ?? Empty<MemberRef>.EmptyList;
        }
    }
}
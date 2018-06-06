using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct InterfaceDoc
    {
        public readonly MemberDoc Member;

        public readonly IReadOnlyList<MemberRef> Events;
        public readonly IReadOnlyList<MemberRef> Properties;
        public readonly IReadOnlyList<MemberRef> Methods;

        public InterfaceDoc(in MemberDoc member,
            IReadOnlyList<MemberRef> events,
            IReadOnlyList<MemberRef> properties,
            IReadOnlyList<MemberRef> methods)
        {
            Member = member;

            Events = events ?? Empty<MemberRef>.List;
            Properties = properties ?? Empty<MemberRef>.List;
            Methods = methods ?? Empty<MemberRef>.List;
        }
    }
}
using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct AssemblyComments
    {
        public readonly string Name;

        public readonly IReadOnlyDictionary<string, MemberComments> Types;
        public readonly IReadOnlyDictionary<string, MemberComments> Fields;
        public readonly IReadOnlyDictionary<string, MemberComments> Properties;
        public readonly IReadOnlyDictionary<string, MemberComments> Methods;
        public readonly IReadOnlyDictionary<string, MemberComments> Events;

        public AssemblyComments(string name,
            IReadOnlyDictionary<string, MemberComments> types,
            IReadOnlyDictionary<string, MemberComments> fields,
            IReadOnlyDictionary<string, MemberComments> properties,
            IReadOnlyDictionary<string, MemberComments> methods,
            IReadOnlyDictionary<string, MemberComments> events
        )
        {
            Name = name ?? "";

            Types = types ?? Empty<string, MemberComments>.EmptyDict;
            Fields = fields ?? Empty<string, MemberComments>.EmptyDict;
            Properties = properties ?? Empty<string, MemberComments>.EmptyDict;
            Methods = methods ?? Empty<string, MemberComments>.EmptyDict;
            Events = events ?? Empty<string, MemberComments>.EmptyDict;
        }

        public static AssemblyComments Empty
            => new AssemblyComments(null, null, null, null, null, null);
    }
}
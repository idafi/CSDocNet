using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct AssemblyComments
    {
        public readonly string Name;
        public readonly IReadOnlyDictionary<string, MemberComments> Members;

        public AssemblyComments(string name, IReadOnlyDictionary<string, MemberComments> members)
        {
            Name = name ?? "";
            Members = members ?? Empty<string, MemberComments>.EmptyDict;
        }

        public static AssemblyComments Empty
            => new AssemblyComments(null, null);
    }
}
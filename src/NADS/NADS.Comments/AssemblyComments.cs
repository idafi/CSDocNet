using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Comments
{
    public readonly struct AssemblyComments
    {
        public readonly string Name;
        public readonly IReadOnlyList<MemberComments> Members;

        public AssemblyComments(string name, IReadOnlyList<MemberComments> members)
        {
            Name = name ?? "";
            Members = members ?? Empty<MemberComments>.EmptyList;
        }

        public static AssemblyComments Empty
            => new AssemblyComments(null, null);
    }
}
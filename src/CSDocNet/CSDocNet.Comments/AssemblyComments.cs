using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Comments
{
    public class AssemblyComments
    {
        public readonly string Name;
        public readonly IReadOnlyDictionary<string, MemberComments> Members;

        public AssemblyComments(string name, IReadOnlyDictionary<string, MemberComments> members)
        {
            Name = name ?? "";
            Members = members ?? Empty<string, MemberComments>.Dict;
        }

        public static AssemblyComments Empty
            => new AssemblyComments(null, null);
    }
}
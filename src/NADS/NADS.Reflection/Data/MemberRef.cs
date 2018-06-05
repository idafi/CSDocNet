using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MemberRef
    {
        public readonly MemberRefType Type;
        public readonly string Name;

        public readonly IReadOnlyList<int> ArrayDimensions;
        
        public MemberRef(MemberRefType type, string name, IReadOnlyList<int> arrayDimensions = null)
        {
            Type = type;
            Name = name ?? "";

            ArrayDimensions = arrayDimensions ?? Empty<int>.EmptyList;
        }
    }
}
using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MemberRef
    {
        public readonly MemberRefType Type;
        public readonly int Token;

        public readonly IReadOnlyList<int> ArrayDimensions;
        
        public MemberRef(MemberRefType type, int token, IReadOnlyList<int> arrayDimensions = null)
        {
            Type = type;
            Token = token;

            ArrayDimensions = arrayDimensions ?? Empty<int>.List;
        }
    }
}
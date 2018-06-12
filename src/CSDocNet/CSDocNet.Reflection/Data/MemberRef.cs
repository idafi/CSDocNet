using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public readonly struct MemberRef
    {
        public readonly MemberRefType Type;
        public readonly int ID;

        public readonly IReadOnlyList<int> ArrayDimensions;
        
        public MemberRef(MemberRefType type, int token, IReadOnlyList<int> arrayDimensions = null)
        {
            Type = type;
            Token = token;

            ArrayDimensions = arrayDimensions ?? Empty<int>.List;
        }
    }
}
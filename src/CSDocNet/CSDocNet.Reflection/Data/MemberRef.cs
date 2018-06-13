using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class MemberRef
    {
        public readonly MemberRefType Type;
        public readonly int ID;

        public readonly IReadOnlyList<int> ArrayDimensions;
        public readonly IReadOnlyList<MemberRef> TypeParams;
        
        public MemberRef(MemberRefType type, int id,
            IReadOnlyList<int> arrayDimensions = null, IReadOnlyList<MemberRef> typeParams = null)
        {
            Type = type;
            ID = id;

            ArrayDimensions = arrayDimensions ?? Empty<int>.List;
            TypeParams = typeParams ?? Empty<MemberRef>.List;
        }
    }
}
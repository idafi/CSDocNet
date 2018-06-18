using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class MemberRef
    {
        // always used
        public readonly string Name;

        // enables hyperlinks, if type can be resolved
        public readonly MemberRefType Type;
        public readonly int ID;

        public readonly IReadOnlyList<int> ArrayDimensions;
        public readonly IReadOnlyList<TypeParamRef> TypeParams;
        
        public MemberRef(string name, MemberRefType type, int id,
            IReadOnlyList<int> arrayDimensions = null, IReadOnlyList<TypeParamRef> typeParams = null)
        {
            Name = name;
            
            Type = type;
            ID = id;

            ArrayDimensions = arrayDimensions ?? Empty<int>.List;
            TypeParams = typeParams ?? Empty<TypeParamRef>.List;
        }
    }
}
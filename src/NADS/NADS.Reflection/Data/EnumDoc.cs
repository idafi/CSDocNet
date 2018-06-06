using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct EnumDoc
    {
        public readonly MemberDoc Member;

        public readonly MemberRef UnderlyingType;
        public readonly IReadOnlyList<EnumValue> Values;

        public EnumDoc(in MemberDoc member, in MemberRef underlyingType,
            IReadOnlyList<EnumValue> values)
        {
            Member = member;
            
            UnderlyingType = underlyingType;
            Values = values ?? Empty<EnumValue>.List;
        }
    }
}
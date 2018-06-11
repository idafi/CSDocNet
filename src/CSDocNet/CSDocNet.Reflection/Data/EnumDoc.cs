using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public readonly struct EnumData
    {
        public readonly MemberData Member;

        public readonly MemberRef UnderlyingType;
        public readonly IReadOnlyList<EnumValue> Values;

        public EnumData(in MemberData member, in MemberRef underlyingType,
            IReadOnlyList<EnumValue> values)
        {
            Member = member;
            
            UnderlyingType = underlyingType;
            Values = values ?? Empty<EnumValue>.List;
        }
    }
}
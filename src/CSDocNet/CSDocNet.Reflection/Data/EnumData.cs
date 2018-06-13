using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class EnumData
    {
        public readonly MemberData Member;

        public readonly MemberRef UnderlyingType;
        public readonly IReadOnlyList<EnumValue> Values;

        public EnumData(MemberData member, MemberRef underlyingType,
            IReadOnlyList<EnumValue> values)
        {
            Member = member;
            
            UnderlyingType = underlyingType;
            Values = values ?? Empty<EnumValue>.List;
        }
    }
}
namespace CSDocNet.Reflection.Data
{
    public readonly struct FieldData
    {
        public readonly MemberData Member;
        public readonly object ConstValue;

        public FieldData(in MemberData member, object constValue)
        {
            Member = member;
            ConstValue = constValue;
        }
    }
}
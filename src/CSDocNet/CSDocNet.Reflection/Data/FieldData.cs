namespace CSDocNet.Reflection.Data
{
    public class FieldData
    {
        public readonly MemberData Member;
        public readonly object ConstValue;

        public FieldData(MemberData member, object constValue)
        {
            Member = member;
            ConstValue = constValue;
        }
    }
}
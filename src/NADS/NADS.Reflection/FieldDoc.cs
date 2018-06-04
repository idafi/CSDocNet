namespace NADS.Reflection
{
    public readonly struct FieldDoc
    {
        public readonly MemberDoc Member;
        public readonly object ConstValue;

        public FieldDoc(in MemberDoc member, object constValue)
        {
            Member = member;
            ConstValue = constValue;
        }
    }
}
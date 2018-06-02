namespace NADS.Reflection
{
    public readonly struct FieldDoc
    {
        public readonly MemberDoc Member;

        public readonly bool HasDefaultValue;
        public readonly object DefaultValue;

        public FieldDoc(in MemberDoc member, 
            bool hasDefaultValue, object defaultValue)
        {
            Member = member;

            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
        }
    }
}
namespace NADS.Reflection.Data
{
    public readonly struct PropertyDoc
    {
        public readonly MemberDoc Member;
        public readonly FieldDoc Field;

        public readonly bool HasGetAccessor;
        public readonly bool HasSetAccessor;

        public PropertyDoc(in MemberDoc member, in FieldDoc field,
            bool hasGetAccessor, bool hasSetAccessor)
        {
            Member = member;
            Field = field;

            HasGetAccessor = hasGetAccessor;
            HasSetAccessor = hasSetAccessor;
        }
    }
}
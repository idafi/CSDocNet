namespace NADS.Reflection.Data
{
    public readonly struct PropertyDoc
    {
        public readonly MemberDoc Member;

        public readonly bool HasGetAccessor;
        public readonly bool HasSetAccessor;

        public PropertyDoc(in MemberDoc member,
            bool hasGetAccessor, bool hasSetAccessor)
        {
            Member = member;
            
            HasGetAccessor = hasGetAccessor;
            HasSetAccessor = hasSetAccessor;
        }
    }
}
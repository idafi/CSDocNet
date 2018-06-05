namespace NADS.Reflection.Data
{
    public readonly struct PropertyDoc
    {
        public readonly struct Accessor
        {
            public readonly bool IsDefined;
            public readonly AccessModifier Access;

            public Accessor(bool isDefined, AccessModifier access)
            {
                IsDefined = isDefined;
                Access = access;
            }
        }

        public readonly MemberDoc Member;

        public readonly Accessor GetAccessor;
        public readonly Accessor SetAccessor;

        public PropertyDoc(in MemberDoc member, in Accessor getAccessor, in Accessor setAccessor)
        {
            Member = member;

            GetAccessor = getAccessor;
            SetAccessor = setAccessor;
        }
    }
}
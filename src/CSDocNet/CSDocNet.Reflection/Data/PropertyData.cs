namespace CSDocNet.Reflection.Data
{
    public readonly struct PropertyData
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

        public readonly MemberData Member;

        public readonly Accessor GetAccessor;
        public readonly Accessor SetAccessor;

        public PropertyData(in MemberData member, in Accessor getAccessor, in Accessor setAccessor)
        {
            Member = member;

            GetAccessor = getAccessor;
            SetAccessor = setAccessor;
        }
    }
}
namespace CSDocNet.Reflection.Data
{
    public class PropertyData
    {
        public class Accessor
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

        public PropertyData(MemberData member, Accessor getAccessor, Accessor setAccessor)
        {
            Member = member;

            GetAccessor = getAccessor;
            SetAccessor = setAccessor;
        }
    }
}
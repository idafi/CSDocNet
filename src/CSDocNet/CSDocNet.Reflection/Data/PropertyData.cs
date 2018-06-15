using System.Collections.Generic;

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
        public readonly IReadOnlyList<Param> IndexerParams;

        public readonly Accessor GetAccessor;
        public readonly Accessor SetAccessor;

        public PropertyData(MemberData member, IReadOnlyList<Param> indexerParams,
            Accessor getAccessor, Accessor setAccessor)
        {
            Member = member;
            IndexerParams = indexerParams;

            GetAccessor = getAccessor;
            SetAccessor = setAccessor;
        }
    }
}
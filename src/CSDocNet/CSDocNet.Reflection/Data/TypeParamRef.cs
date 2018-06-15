namespace CSDocNet.Reflection.Data
{
    public class TypeParamRef
    {
        public readonly MemberRefType DeclaringType;
        public readonly int DeclaredPosition;           // -1 if constructed

        public readonly MemberRef ConstructedType;       // null if generic

        public bool IsGeneric => (DeclaredPosition > -1);
        public bool IsConstructed => (ConstructedType != null);

        public TypeParamRef(MemberRefType declaringtype, int declaredPosition)
        {
            DeclaringType = declaringtype;
            DeclaredPosition = declaredPosition;

            ConstructedType = null;
        }

        public TypeParamRef(MemberRef constructedType)
        {
            DeclaringType = default;
            DeclaredPosition = -1;

            ConstructedType = constructedType;
        }
    }
}
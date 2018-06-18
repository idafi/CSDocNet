namespace CSDocNet.Reflection.Data
{
    public class TypeParamRef
    {
        public readonly string TypeParamName;
        public readonly MemberRef MemberRef;
        
        public bool IsGenericParam => TypeParamName != null;
        public bool IsConstructedParam => MemberRef != null;
        
        public TypeParamRef(string typeParamName)
        {
            TypeParamName = typeParamName;
        }
        
        public TypeParamRef(MemberRef memberRef)
        {
            MemberRef = memberRef;
        }
    }
}
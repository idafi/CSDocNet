namespace CSDocNet.Reflection.Data
{
    public enum MemberRefType
    {
        Class,
        Struct,
        Interface,
        Enum,
        Delegate,
        
        Event,
        Field,
        Property,
        Constructor,
        Operator,
        Method,

        // "why both?" position is different for each
        TypeParam,
        MethodTypeParam
    }
}
namespace CSDocNet.Reflection.Data
{
    public readonly struct ReturnValue
    {
        public readonly ReturnModifier Modifier;
        public readonly MemberRef Type;

        public readonly bool IsGenericType;
        public readonly int GenericTypePosition;

        public ReturnValue(ReturnModifier modifier, in MemberRef type,
            bool isGenericType, int genericTypePosition)
        {
            Modifier = modifier;
            Type = type;

            IsGenericType = isGenericType;
            GenericTypePosition = genericTypePosition;
        }
    }
}
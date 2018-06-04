namespace NADS.Reflection.Data
{
    public readonly struct Param
    {
        public readonly ParamModifier Modifier;
        public readonly MemberRef Type;

        public readonly bool IsGenericType;
        public readonly int GenericTypePosition;

        public readonly bool HasDefaultValue;
        public readonly object DefaultValue;

        public Param(ParamModifier modifier, in MemberRef type,
            bool isGenericType, int genericTypePosition,
            bool hasDefaultValue, object defaultValue)
        {
            Modifier = modifier;
            Type = type;

            IsGenericType = isGenericType;
            GenericTypePosition = genericTypePosition;

            HasDefaultValue = hasDefaultValue;
            DefaultValue = defaultValue;
        }
    }
}
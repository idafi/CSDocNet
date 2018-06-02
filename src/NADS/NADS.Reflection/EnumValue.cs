namespace NADS.Reflection
{
    public readonly struct EnumValue
    {
        public readonly string Name;
        public readonly long Value;

        public EnumValue(string name, long value)
        {
            Name = name ?? "";
            Value = value;
        }
    }
}
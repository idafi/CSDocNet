namespace CSDocNet.Reflection.Data
{
    public class EnumValue
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
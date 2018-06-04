namespace NADS.Reflection.Data
{
    public readonly struct MemberRef
    {
        public readonly MemberRefType Type;
        public readonly string Name;
        
        public MemberRef(MemberRefType type, string name)
        {
            Type = type;
            Name = name ?? "";
        }
    }
}
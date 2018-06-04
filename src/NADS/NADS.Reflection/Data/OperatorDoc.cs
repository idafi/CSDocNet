namespace NADS.Reflection.Data
{
    public readonly struct OperatorDoc
    {
        public readonly MemberDoc Member;
        public readonly MethodDoc Method;

        public readonly Operator Operator;

        public OperatorDoc(in MemberDoc member, in MethodDoc method,
            Operator @operator)
        {
            Member = member;
            Method = method;

            Operator = @operator;
        }
    }
}
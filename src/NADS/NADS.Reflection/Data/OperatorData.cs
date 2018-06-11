namespace NADS.Reflection.Data
{
    public readonly struct OperatorData
    {
        public readonly MemberData Member;
        public readonly MethodData Method;

        public readonly Operator Operator;

        public OperatorData(in MemberData member, in MethodData method,
            Operator @operator)
        {
            Member = member;
            Method = method;

            Operator = @operator;
        }
    }
}
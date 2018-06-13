namespace CSDocNet.Reflection.Data
{
    public class OperatorData
    {
        public readonly MemberData Member;
        public readonly MethodData Method;

        public readonly Operator Operator;

        public OperatorData(MemberData member, MethodData method,
            Operator @operator)
        {
            Member = member;
            Method = method;

            Operator = @operator;
        }
    }
}
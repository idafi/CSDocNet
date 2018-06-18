using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public interface IMDMemberRefUtility
    {
        string GetMemberName(MemberRef mRef, AssemblyData assemblyData);

        string GetClassName(ClassData data, MemberRef cRef, AssemblyData assemblyData);
        string GetOperatorName(OperatorData data, MemberRef opRef, AssemblyData assemblyData);
        string GetMethodName(MethodData data, MemberRef pRef, AssemblyData assemblyData);
        string GetMemberName(MemberData data, MemberRef mRef, AssemblyData assemblyData);
    }
}
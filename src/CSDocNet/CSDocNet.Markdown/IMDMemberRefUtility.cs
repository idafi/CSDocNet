using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public interface IMDMemberRefUtility
    {
        string GetMemberName(MemberRef mRef, AssemblyData assemblyData);
        string GetOperatorName(MemberRef opRef, AssemblyData assemblyData);
    }
}
using CSDocNet.Comments;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public interface IMDSummaryWriter
    {
        string WriteClassSummary(ClassData classData, AssemblyData assemblyData, MemberComments comments);
        string WriteStructSummary(ClassData structData, AssemblyData assemblyData, MemberComments comments);
        string WriteInterfaceSummary(ClassData interfaceData, AssemblyData assemblyData, MemberComments comments);
        string WriteEnumSummary(EnumData enumData, AssemblyData assemblyData, MemberComments comments);
        string WriteDelegateSummary(MethodData delegateData, AssemblyData assemblyData, MemberComments comments);

        string WriteEventSummary(MemberData eventData, AssemblyData assemblyData, MemberComments comments);
        string WriteFieldSummary(FieldData fieldData, AssemblyData assemblyData, MemberComments comments);
        string WritePropertySummary(PropertyData propertyData, AssemblyData assemblyData, MemberComments comments);
        string WriteConstructorSummary(MethodData ctorData, AssemblyData assemblyData, MemberComments comments);
        string WriteOperatorSummary(OperatorData operatorData, AssemblyData assemblyData, MemberComments comments);
        string WriteMethodSummary(MethodData methodData, AssemblyData assemblyData, MemberComments comments);

        string WriteSummaryComments(MemberData memberData, AssemblyComments comments);
        string WriteSyntax(MemberData memberData, AssemblyData assemblyData, MemberComments comments);
        string WriteTypeParams(MethodData methodData, AssemblyData assemblyData, MemberComments comments);
        string WriteTypeParams(ClassData classData, AssemblyData assemblyData, MemberComments comments);
        string WriteParams(MethodData methodData, AssemblyData assemblyData, MemberComments comments);
        string WriteValue(MethodData methodData, AssemblyData assemblyData, MemberComments comments);
        string WriteReturns(MethodData methodData, AssemblyData assemblyData, MemberComments comments);
        string WriteExceptions(MemberData data, AssemblyData assemblyData, MemberComments comments);
        string WritePermissions(MemberData data, AssemblyData assemblyData, MemberComments comments);
    }
}
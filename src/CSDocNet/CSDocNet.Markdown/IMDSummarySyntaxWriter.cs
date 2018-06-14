using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public interface IMDSummarySyntaxWriter
    {
        string WriteClassSyntax(ClassData classData, AssemblyData assemblyData);
        string WriteStructSyntax(ClassData structData, AssemblyData assemblyData);
        string WriteInterfaceSyntax(ClassData interfaceData, AssemblyData assemblyData);
        string WriteEnumSyntax(EnumData enumData, AssemblyData assemblyData);
        string WriteDelegateSyntax(MethodData delegateData, AssemblyData assemblyData);

        string WriteEventSyntax(MemberData eventData, AssemblyData assemblyData);
        string WriteFieldSyntax(FieldData fieldData, AssemblyData assemblyData);
        string WritePropertySyntax(PropertyData propertyData, AssemblyData assemblyData);
        string WriteConstructorSyntax(MethodData ctorData, AssemblyData assemblyData);
        string WriteOperatorSyntax(OperatorData operatorData, AssemblyData assemblyData);
        string WriteMethodSyntax(MethodData methodData, AssemblyData assemblyData);

        string WriteAttributes(MemberData member, AssemblyData assemblyData);
        string WriteAccess(MemberData member);
        string WriteModifiers(MemberData member);
        string WriteTypeParams(MemberData member, AssemblyData assemblyData);
        string WriteInheritedTypes(ClassData classData, AssemblyData assembly);
        string WriteConstraints(MemberData member, AssemblyData assembly);
    }
}
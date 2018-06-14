using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public interface IMDSummarySyntaxWriter
    {
        string WriteClassSyntax(ClassData classData);
        string WriteStructSyntax(ClassData structData);
        string WriteInterfaceSyntax(ClassData interfaceData);
        string WriteEnumSyntax(EnumData enumData);
        string WriteDelegateSyntax(MethodData delegateData);

        string WriteEventSyntax(MemberData eventData);
        string WriteFieldSyntax(FieldData fieldData);
        string WritePropertySyntax(PropertyData propertyData);
        string WriteConstructorSyntax(MethodData ctorData);
        string WriteOperatorSyntax(OperatorData operatorData);
        string WriteMethodSyntax(MethodData methodData);

        string WriteAttributes(MemberData member);
        string WriteAccess(MemberData member);
        string WriteModifiers(MemberData member);
        string WriteTypeParams(MemberData member);
        string WriteInheritedTypes(ClassData classData, AssemblyData assembly);
        string WriteConstraints(MemberData member, AssemblyData assembly);
    }
}
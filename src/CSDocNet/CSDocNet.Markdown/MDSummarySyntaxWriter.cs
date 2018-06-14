using CSDocNet.Comments;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public class MDSummarySyntaxWriter : IMDSummarySyntaxWriter
    {
        public string WriteClassSyntax(ClassData classData, AssemblyData assemblyData)
        {

        }

        public string WriteStructSyntax(ClassData structData, AssemblyData assemblyData)
        {

        }

        public string WriteInterfaceSyntax(ClassData interfaceData, AssemblyData assemblyData)
        {

        }

        public string WriteEnumSyntax(EnumData enumData, AssemblyData assemblyData)
        {

        }

        public string WriteDelegateSyntax(MethodData delegateData, AssemblyData assemblyData)
        {

        }

        public string WriteEventSyntax(MemberData eventData, AssemblyData assemblyData)
        {

        }

        public string WriteFieldSyntax(FieldData fieldData, AssemblyData assemblyData)
        {

        }

        public string WritePropertySyntax(PropertyData propertyData, AssemblyData assemblyData)
        {

        }

        public string WriteConstructorSyntax(MethodData ctorData, AssemblyData assemblyData)
        {

        }

        public string WriteOperatorSyntax(OperatorData operatorData, AssemblyData assemblyData)
        {

        }

        public string WriteMethodSyntax(MethodData methodData, AssemblyData assemblyData)
        {

        }

        public string WriteAttributes(MemberData member, AssemblyData assemblyData)
        {

        }

        public string WriteAccess(MemberData member)
        {
            switch(member.Access)
            {
                case AccessModifier.Public:
                    return "public";
                case AccessModifier.ProtectedInternal:
                    return "protected internal";
                case AccessModifier.Internal:
                    return "internal";
                case AccessModifier.Protected:
                    return "protected";
                case AccessModifier.PrivateProtected:
                    return "private protected";
                case AccessModifier.Private:
                    return "private";
                default:
                    return "";
            }
        }

        public string WriteModifiers(MemberData member)
        {
            Modifier mods = member.Modifiers;
            string modStr = "";

            if(mods.HasFlag(Modifier.Abstract))
            { modStr += "abstract "; }
            if(mods.HasFlag(Modifier.Async))
            { modStr += "async "; }
            if(mods.HasFlag(Modifier.Const))
            { modStr += "const "; }
            if(mods.HasFlag(Modifier.Extern))
            { modStr += "extern "; }
            if(mods.HasFlag(Modifier.Override))
            { modStr += "override "; }
            if(mods.HasFlag(Modifier.Readonly))
            { modStr += "readonly "; }
            if(mods.HasFlag(Modifier.Ref))
            { modStr += "ref "; }
            if(mods.HasFlag(Modifier.Sealed))
            { modStr += "sealed "; }
            if(mods.HasFlag(Modifier.Static))
            { modStr += "static "; }
            if(mods.HasFlag(Modifier.Virtual))
            { modStr += "virtual "; }
            if(mods.HasFlag(Modifier.Volatile))
            { modStr += "volatile "; }

            return modStr.TrimEnd();
        }

        public string WriteTypeParams(MemberData member, AssemblyData assemblyData)
        {

        }

        public string WriteInheritedTypes(ClassData classData, AssemblyData assembly)
        {
            string str = "";

            if(classData.Inherits != null)
            {
                MemberRef mRef = classData.Inherits;
                switch(mRef.Type)
                {
                    case MemberRefType.Class:
                        ClassData cData = assembly.Classes[mRef.ID];
                        
                }
            }
        }

        public string WriteConstraints(MemberData member, AssemblyData assembly)
        {
            
        }
    }
}
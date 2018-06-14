using System;
using CSDocNet.Comments;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public class MDSummarySyntaxWriter : IMDSummarySyntaxWriter
    {
        public string WriteClassSyntax(ClassData classData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteStructSyntax(ClassData structData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteInterfaceSyntax(ClassData interfaceData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteEnumSyntax(EnumData enumData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteDelegateSyntax(MethodData delegateData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteEventSyntax(MemberData eventData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteFieldSyntax(FieldData fieldData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WritePropertySyntax(PropertyData propertyData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteConstructorSyntax(MethodData ctorData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteOperatorSyntax(OperatorData operatorData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteMethodSyntax(MethodData methodData, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
        }

        public string WriteAttributes(MemberData member, AssemblyData assemblyData)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string WriteInheritedTypes(ClassData classData, AssemblyData assembly)
        {
            throw new NotImplementedException();
        }

        public string WriteConstraints(MemberData member, AssemblyData assembly)
        {
            throw new NotImplementedException();
        }
    }
}
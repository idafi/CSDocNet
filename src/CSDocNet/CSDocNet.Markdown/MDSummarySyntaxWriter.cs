using System;
using System.Collections.Generic;
using CSDocNet.Comments;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public class MDSummarySyntaxWriter : IMDSummarySyntaxWriter
    {
        readonly IMDMemberRefUtility refUtility;

        public MDSummarySyntaxWriter(IMDMemberRefUtility refUtility)
        {
            Check.Ref(refUtility);

            this.refUtility = refUtility;
        }

        public string WriteClassSyntax(ClassData classData, AssemblyData assemblyData)
        {
            return WriteClassData(classData, "class", assemblyData);
        }

        public string WriteStructSyntax(ClassData structData, AssemblyData assemblyData)
        {
            return WriteClassData(structData, "struct", assemblyData);
        }

        public string WriteInterfaceSyntax(ClassData interfaceData, AssemblyData assemblyData)
        {
            return WriteClassData(interfaceData, "interface", assemblyData);
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
            string str = "";

            foreach(MemberRef attr in member.Attributes)
            { str += $"\n\t[{refUtility.GetMemberName(attr, assemblyData)}]"; }

            return str;
        }

        public string WriteAccess(MemberData member)
        {
            switch(member.Access)
            {
                case AccessModifier.Public:
                    return "public ";
                case AccessModifier.ProtectedInternal:
                    return "protected internal ";
                case AccessModifier.Internal:
                    return "internal ";
                case AccessModifier.Protected:
                    return "protected ";
                case AccessModifier.PrivateProtected:
                    return "private protected ";
                case AccessModifier.Private:
                    return "private ";
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

            return modStr;
        }

        public string WriteTypeParams(ClassData classData)
        {
            return WriteTypeParams(classData.TypeParams);
        }

        public string WriteTypeParams(MethodData methodData)
        {
            return WriteTypeParams(methodData.TypeParams);
        }

        public string WriteInheritedTypes(ClassData classData, AssemblyData assemblyData)
        {
            if(!ClassInherits(classData) && classData.Implements.Count <= 0)
            { return ""; }

            List<string> types = new List<string>(classData.Implements.Count + 1);
            if(ClassInherits(classData))
            { types.Add(refUtility.GetMemberName(classData.Inherits, assemblyData)); }
            foreach(MemberRef implements in classData.Implements)
            { types.Add(refUtility.GetMemberName(implements, assemblyData)); }

            return $" : {string.Join(", ", types)}";
        }

        public string WriteConstraints(ClassData classData, AssemblyData assemblyData)
        {
            string str = "";

            for(int i = 0; i < classData.TypeParams.Count; i++)
            {
                TypeParam tp = classData.TypeParams[i];
                str += WriteTypeConstraints(tp.Constraints, i, classData.TypeParams, assemblyData);
            }

            return str;
        }

        string WriteClassData(ClassData data, string keyword, AssemblyData assemblyData)
        {
            string str = "";

            str += WriteAttributes(data.Member, assemblyData);
            str += "\n\t";
            str += WriteAccess(data.Member);
            str += WriteModifiers(data.Member);
            str += $"{keyword} ";
            str += data.Member.Name;
            str += WriteTypeParams(data);
            str += WriteInheritedTypes(data, assemblyData);
            str += WriteConstraints(data, assemblyData);

            return str;
        }

        string WriteTypeParams(IReadOnlyList<TypeParam> typeParams)
        {
            if(typeParams.Count <= 0)
            { return ""; }

            return $"<{string.Join(", ", GetTypeParamNames(typeParams))}>";
        }

        string WriteTypeConstraints(IReadOnlyList<TypeConstraint> typeConstraints, int constrainedParam,
            IReadOnlyList<TypeParam> typeParams, AssemblyData assemblyData)
        {
            if(typeConstraints.Count <= 0)
            { return ""; }

            var tpNames = GetTypeConstraintNames(typeConstraints, typeParams, assemblyData);
            return $"\n\twhere {typeParams[constrainedParam].Name} : {string.Join(", ", tpNames)}";
        }

        IEnumerable<string> GetTypeParamNames(IEnumerable<TypeParam> typeParams)
        {
            foreach(TypeParam tp in typeParams)
            { yield return tp.Name; }
        }

        IEnumerable<string> GetTypeConstraintNames(IEnumerable<TypeConstraint> typeConstraints,
            IReadOnlyList<TypeParam> typeParams, AssemblyData assemblyData)
        {
            foreach(TypeConstraint tc in typeConstraints)
            {
                switch(tc.Constraint)
                {
                    case ConstraintType.Class:
                        yield return "class";
                        break;
                    case ConstraintType.Struct:
                        yield return "struct";
                        break;
                    case ConstraintType.Ctor:
                        yield return "new()";
                        break;
                    case ConstraintType.Type:
                        yield return refUtility.GetMemberName(tc.ConstrainedType, assemblyData);
                        break;
                    case ConstraintType.TypeParam:
                        yield return typeParams[tc.ConstrainedTypeParamPosition].Name;
                        break;
                }
            }
        }

        bool ClassInherits(ClassData classData)
        {
            return (classData.Inherits != null
            && classData.Inherits.ID != typeof(object).MetadataToken
            && classData.Inherits.ID != typeof(ValueType).MetadataToken
            && classData.Inherits.ID != typeof(Delegate).MetadataToken);
        }
    }
}
using System;
using System.Collections.Generic;
using CSDocNet.Debug;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Markdown
{
    public class MDMemberRefUtility : IMDMemberRefUtility
    {
        public string GetMemberName(MemberRef mRef, AssemblyData assemblyData)
        {
            string name = GetMemberNameBase(mRef, assemblyData);

            foreach(int arrayDim in mRef.ArrayDimensions)
            { name += $"[{new string(',', arrayDim - 1)}]"; }

            return name;
        }

        public string GetClassName(ClassData data, MemberRef cRef, AssemblyData assemblyData)
        {
            string name = data.Member.Name;
            string tpList = GetTypeParamList(data.TypeParams, cRef.TypeParams, assemblyData);
            
            return name + tpList;
        }

        public string GetOperatorName(OperatorData data, MemberRef opRef, AssemblyData assemblyData)
        {
            switch(data.Operator)
            {
                case Operator.Implicit:
                case Operator.Explicit:
                    return GetMemberName(data.Method.ReturnValue.Type, assemblyData);
                default:
                    return data.Operator.ToString();
            }
        }

        public string GetMethodName(MethodData data, MemberRef mRef, AssemblyData assemblyData)
        {
            string name = data.Member.Name;
            string tpList = GetTypeParamList(data.TypeParams, mRef.TypeParams, assemblyData);

            return name + tpList;
        }

        public string GetMemberName(MemberData data, MemberRef mRef, AssemblyData assemblyData)
        {
            return data.Name;
        }

        string GetMemberNameBase(MemberRef mRef, AssemblyData assemblyData)
        {
            switch(mRef.Type)
            {
                case MemberRefType.Class:
                    return GetName(assemblyData.Classes, mRef, assemblyData, GetClassName);
                case MemberRefType.Struct:
                    return GetName(assemblyData.Structs, mRef, assemblyData, GetClassName);
                case MemberRefType.Interface:
                    return GetName(assemblyData.Interfaces, mRef, assemblyData, GetClassName);
                case MemberRefType.Enum:
                    return GetName(assemblyData.Enums, mRef, assemblyData, (e, r, a) => GetMemberName(e.Member, r, a));
                case MemberRefType.Delegate:
                    return GetName(assemblyData.Delegates, mRef, assemblyData, GetMethodName);
                case MemberRefType.Event:
                    return GetName(assemblyData.Events, mRef, assemblyData, GetMemberName);
                case MemberRefType.Field:
                    return GetName(assemblyData.Fields, mRef, assemblyData, (e, r, a) => GetMemberName(e.Member, r, a));
                case MemberRefType.Property:
                    return GetName(assemblyData.Properties, mRef, assemblyData, (e, r, a) => GetMemberName(e.Member, r, a));
                case MemberRefType.Constructor:
                    return GetName(assemblyData.Constructors, mRef, assemblyData, (e, r, a) => GetMemberName(e.Member, r, a));
                case MemberRefType.Operator:
                    return GetName(assemblyData.Operators, mRef, assemblyData, GetOperatorName);
                case MemberRefType.Method:
                    return GetName(assemblyData.Methods, mRef, assemblyData, GetMethodName);
                default:
                    throw new NotSupportedException($"unknown member ref type '{mRef.Type}'");
            }
        }

        string GetName<T>(IReadOnlyDictionary<int, T> dict, MemberRef mRef, AssemblyData assemblyData,
            Func<T, MemberRef, AssemblyData, string> getter)
        {
            if(dict.TryGetValue(mRef.ID, out T value))
            { return getter(value, mRef, assemblyData); }

            return mRef.Name;
        }

        string GetTypeParamList(IReadOnlyList<TypeParam> declaredParams,
            IReadOnlyList<TypeParamRef> tpRefs, AssemblyData assemblyData)
        {
            if(tpRefs.Count <= 0)
            { return ""; }

            IEnumerable<string> tpNames = GetTypeParamNames(declaredParams, tpRefs, assemblyData);
            return $"<{string.Join(",", tpNames)}>";
        }

        IEnumerable<string> GetTypeParamNames(IReadOnlyList<TypeParam> declaredParams,
            IReadOnlyList<TypeParamRef> tpRefs, AssemblyData assemblyData)
        {
            Assert.Ref(declaredParams, tpRefs, assemblyData);

            foreach(TypeParamRef tpRef in tpRefs)
            {
                if(tpRef.IsGenericParam)
                { yield return tpRef.TypeParamName; }
                else if(tpRef.IsConstructedParam)
                { yield return GetMemberName(tpRef.MemberRef, assemblyData); }
            }
        }
    }
}
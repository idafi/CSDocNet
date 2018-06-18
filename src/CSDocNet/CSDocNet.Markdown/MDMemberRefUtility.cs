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

        public string GetOperatorName(MemberRef opRef, AssemblyData assemblyData)
        {
            if(assemblyData.Operators.TryGetValue(opRef.ID, out var data))
            {
                switch(data.Operator)
                {
                    case Operator.Implicit:
                    case Operator.Explicit:
                        return GetMemberName(data.Method.ReturnValue.Type, assemblyData);
                }
            }

            return data.Operator.ToString();
        }

        string GetMemberNameBase(MemberRef mRef, AssemblyData assemblyData)
        {
            switch(mRef.Type)
            {
                case MemberRefType.Operator:
                    return GetOperatorName(mRef, assemblyData);
                default:
                    return GetName(mRef, assemblyData);
            }
        }

        string GetName(MemberRef mRef, AssemblyData assemblyData)
        {
            return mRef.Name + GetTypeParamList(mRef.TypeParams, assemblyData);
        }

        string GetTypeParamList(IReadOnlyList<TypeParamRef> tpRefs, AssemblyData assemblyData)
        {
            if(tpRefs.Count <= 0)
            { return ""; }

            IEnumerable<string> tpNames = GetTypeParamNames(tpRefs, assemblyData);
            return $"<{string.Join(",", tpNames)}>";
        }

        IEnumerable<string> GetTypeParamNames(IReadOnlyList<TypeParamRef> tpRefs, AssemblyData assemblyData)
        {
            Assert.Ref(tpRefs, assemblyData);

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
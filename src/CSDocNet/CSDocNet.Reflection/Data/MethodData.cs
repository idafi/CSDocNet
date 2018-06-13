using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public class MethodData
    {
        public readonly MemberData Member;

        public readonly ReturnValue ReturnValue;
        public readonly IReadOnlyList<Param> Params;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public MethodData(MemberData member, ReturnValue returnValue,
            IReadOnlyList<Param> parameters, IReadOnlyList<TypeParam> typeParams)
        {
            Member = member;

            ReturnValue = returnValue;
            Params = parameters ?? Empty<Param>.List;;
            TypeParams = typeParams ?? Empty<TypeParam>.List;
        }
    }
}
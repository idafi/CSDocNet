using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MethodData
    {
        public readonly MemberData Member;

        public readonly ReturnValue ReturnValue;
        public readonly IReadOnlyList<Param> Params;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public MethodData(in MemberData member, in ReturnValue returnValue,
            IReadOnlyList<Param> parameters, IReadOnlyList<TypeParam> typeParams)
        {
            Member = member;

            ReturnValue = returnValue;
            Params = parameters ?? Empty<Param>.List;;
            TypeParams = typeParams ?? Empty<TypeParam>.List;
        }
    }
}
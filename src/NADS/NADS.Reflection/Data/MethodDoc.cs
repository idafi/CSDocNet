using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MethodDoc
    {
        public readonly MemberDoc Member;

        public readonly ReturnValue ReturnValue;
        public readonly IReadOnlyList<Param> Params;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public MethodDoc(in MemberDoc member, in ReturnValue returnValue,
            IReadOnlyList<Param> parameters, IReadOnlyList<TypeParam> typeParams)
        {
            Member = member;

            ReturnValue = returnValue;
            Params = parameters ?? Empty<Param>.List;;
            TypeParams = typeParams ?? Empty<TypeParam>.List;
        }
    }
}
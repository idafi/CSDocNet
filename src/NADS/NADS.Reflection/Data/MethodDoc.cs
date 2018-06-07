using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MethodDoc
    {
        public readonly MemberDoc Member;

        public readonly MemberRef ReturnType;
        public readonly IReadOnlyList<Param> Params;
        public readonly IReadOnlyList<TypeParam> TypeParams;

        public MethodDoc(in MemberDoc member, in MemberRef returnType,
            IReadOnlyList<Param> parameters, IReadOnlyList<TypeParam> typeParams)
        {
            Member = member;

            ReturnType = returnType;
            Params = parameters ?? Empty<Param>.List;;
            TypeParams = typeParams ?? Empty<TypeParam>.List;
        }
    }
}
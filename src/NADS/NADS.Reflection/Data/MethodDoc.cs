using System.Collections.Generic;
using NADS.Collections;

namespace NADS.Reflection.Data
{
    public readonly struct MethodDoc
    {
        public readonly MemberDoc Member;

        public readonly MemberRef ReturnType;
        public readonly IReadOnlyList<Param> Params;

        public MethodDoc(in MemberDoc member, in MemberRef returnType,
            IReadOnlyList<Param> parameters)
        {
            Member = member;

            ReturnType = returnType;
            Params = parameters ?? Empty<Param>.List;;
        }
    }
}
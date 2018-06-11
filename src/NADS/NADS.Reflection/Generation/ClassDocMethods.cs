using System.Collections.Generic;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public readonly struct ClassDocMethods
    {
        public readonly IReadOnlyList<MemberRef> Methods;
        public readonly IReadOnlyList<MemberRef> Operators;

        public ClassDocMethods(IReadOnlyList<MemberRef> methods, IReadOnlyList<MemberRef> operators)
        {
            Methods = methods;
            Operators = operators;
        }
    }
}
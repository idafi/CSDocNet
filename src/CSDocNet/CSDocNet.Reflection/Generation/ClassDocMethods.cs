using System.Collections.Generic;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public readonly struct ClassDataMethods
    {
        public readonly IReadOnlyList<MemberRef> Methods;
        public readonly IReadOnlyList<MemberRef> Operators;

        public ClassDataMethods(IReadOnlyList<MemberRef> methods, IReadOnlyList<MemberRef> operators)
        {
            Methods = methods;
            Operators = operators;
        }
    }
}
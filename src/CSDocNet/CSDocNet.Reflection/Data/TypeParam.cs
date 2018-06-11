using System.Collections.Generic;
using CSDocNet.Collections;

namespace CSDocNet.Reflection.Data
{
    public readonly struct TypeParam
    {
        public readonly string Name;

        public readonly ParamModifier Modifier;
        public readonly IReadOnlyList<TypeConstraint> Constraints;

        public TypeParam(string name, ParamModifier modifier, IReadOnlyList<TypeConstraint> constraints)
        {
            Name = name;

            Modifier = modifier;
            Constraints = constraints ?? Empty<TypeConstraint>.List;
        }
    }
}
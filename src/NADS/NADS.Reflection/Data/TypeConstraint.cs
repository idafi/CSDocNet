namespace NADS.Reflection.Data
{
    public readonly struct TypeConstraint
    {
        public readonly ConstraintType Constraint;
        
        public readonly MemberRef ConstrainedType;
        public readonly int ConstrainedTypeParamPosition;

        TypeConstraint(ConstraintType constraint, in MemberRef constrainedType,
            int constrainedTypeParamPosition)
        {
            Constraint = constraint;
            
            ConstrainedType = constrainedType;
            ConstrainedTypeParamPosition = constrainedTypeParamPosition;
        }

        public static TypeConstraint Struct
            => new TypeConstraint(ConstraintType.Struct, default, default);
        public static TypeConstraint Class
            => new TypeConstraint(ConstraintType.Class, default, default);
        public static TypeConstraint Ctor
            => new TypeConstraint(ConstraintType.Ctor, default, default);
        public static TypeConstraint Unmanaged
            => new TypeConstraint(ConstraintType.Unmanaged, default, default);

        public static TypeConstraint Type(in MemberRef constrainedType)
            => new TypeConstraint(ConstraintType.Type, constrainedType, default);
        public static TypeConstraint TypeParam(int typeParamPosition)
            => new TypeConstraint(ConstraintType.TypeParam, default, typeParamPosition);
    }
}
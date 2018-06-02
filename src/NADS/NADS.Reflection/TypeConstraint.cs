namespace NADS.Reflection
{
    public readonly struct TypeConstraint
    {
        public readonly ConstraintType Constraint;
        
        public readonly MemberRef ConstrainedType;
        public readonly int ConstrainedTypeParam;

        public TypeConstraint(ConstraintType constraint, in MemberRef constrainedType,
            int constrainedTypeParam)
        {
            Constraint = constraint;
            
            ConstrainedType = constrainedType;
            ConstrainedTypeParam = constrainedTypeParam;
        }
    }
}
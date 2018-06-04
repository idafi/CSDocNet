using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Debug;

namespace NADS.Reflection
{
    public class DocGeneratorUtility : IDocGeneratorUtility
    {
        public MemberRef MakeMemberRef(MemberInfo member)
        {
            Check.Ref(member);

            var refType = GetMemberRefType(member);
            var refName = GetMemberName(member);

            return new MemberRef(refType, refName);
        }
        
        public ParamModifier GetGenericParamModifier(GenericParameterAttributes attributes)
        {
            if(attributes.HasFlag(GenericParameterAttributes.Contravariant))
            { return ParamModifier.In; }
            else if(attributes.HasFlag(GenericParameterAttributes.Covariant))
            { return ParamModifier.Out; }
            else
            { return ParamModifier.None; }
        }

        public IReadOnlyList<TypeConstraint> GetTypeParamConstraints(Type typeParam)
        {
            List<TypeConstraint> constraints = new List<TypeConstraint>();
            var attr = typeParam.GenericParameterAttributes;

            if(attr.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint))
            { constraints.Add(TypeConstraint.Struct); }
            else
            {
                if(attr.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
                { constraints.Add(TypeConstraint.Class); }
                if(attr.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
                { constraints.Add(TypeConstraint.Ctor); }
                
                foreach(var constraint in typeParam.GetGenericParameterConstraints())
                {
                    if(constraint.IsGenericParameter)
                    {
                        int pos = constraint.GenericParameterPosition;
                        constraints.Add(TypeConstraint.TypeParam(pos));
                    }
                    else
                    {
                        MemberRef mRef = new MemberRef(GetMemberRefType(constraint), constraint.FullName);
                        constraints.Add(TypeConstraint.Type(mRef));
                    }
                }
            }

            return constraints.ToArray();
        }

        MemberRefType GetMemberRefType(MemberInfo member)
        {
            Assert.Ref(member);

            switch(member.MemberType)
            {
                case MemberTypes.TypeInfo:
                case MemberTypes.NestedType:
                    return GetMemberRefType((Type)(member));
                case MemberTypes.Event:
                    return MemberRefType.Event;
                case MemberTypes.Field:
                    return MemberRefType.Field;
                case MemberTypes.Property:
                    return MemberRefType.Property;
                case MemberTypes.Constructor:
                    return MemberRefType.Constructor;
                case MemberTypes.Method:
                    return MemberRefType.Method;
                default:
                    throw new NotSupportedException($"member '{member.Name}' has an unknown member type");
            }
        }

        string GetMemberName(MemberInfo member)
        {
            switch(member.MemberType)
            {
                case MemberTypes.TypeInfo:
                case MemberTypes.NestedType:
                    Type t = (Type)(member);
                    return t.FullName;
                default:
                    return $"{member.DeclaringType.FullName}.{member.Name}";
            }
        }

        MemberRefType GetMemberRefType(Type type)
        {
            Assert.Ref(type);

            if(type.BaseType == typeof(Delegate) || type.BaseType == typeof(MulticastDelegate))
            { return MemberRefType.Delegate; }
            else if(type.IsClass)
            { return MemberRefType.Class; }
            else if(type.IsInterface)
            { return MemberRefType.Interface; }
            else if(type.IsEnum)
            { return MemberRefType.Enum; }
            else if(type.IsValueType)
            { return MemberRefType.Struct; }
            else
            { throw new NotSupportedException($"couldn't determine member type of type '{type.Name}'"); }
        }
    }
}
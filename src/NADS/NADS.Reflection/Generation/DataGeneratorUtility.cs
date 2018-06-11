using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class DataGeneratorUtility : IDataGeneratorUtility
    {
        public string GenerateName(MemberInfo member)
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

        public IReadOnlyList<MemberRef> GenerateAttributes(MemberInfo memberInfo)
        {
            Check.Ref(memberInfo);

            List<MemberRef> attributes = new List<MemberRef>();
            foreach(var attr in memberInfo.CustomAttributes)
            {
                MemberRef mRef = MakeMemberRef(attr.AttributeType);
                attributes.Add(mRef);
            }

            return attributes.ToArray();
        }

        public Type GetRootElementType(Type type)
        {
            Check.Ref(type);

            var result = FindRootElementType(type);
            return result.Type;
        }

        public MemberRef MakeMemberRef(MemberInfo member)
        {
            Check.Ref(member);

            IReadOnlyList<int> arrayDim = Empty<int>.List;
            if(member is Type type)
            {
                var result = FindRootElementType(type);
                member = result.Type;
                arrayDim = result.Arrays;
            }

            var refType = GetMemberRefType(member);
            return new MemberRef(refType, member.MetadataToken, arrayDim);
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
            Check.Ref(typeParam);
            Check.Cond(typeParam.IsGenericParameter, "typeParam must be a generic parameter type");

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
                        MemberRef mRef = new MemberRef(GetMemberRefType(constraint), constraint.MetadataToken);
                        constraints.Add(TypeConstraint.Type(mRef));
                    }
                }
            }

            return constraints.ToArray();
        }

        public bool IsReadOnly(ICustomAttributeProvider member)
        {
            Check.Ref(member);

            if(member is FieldInfo field)
            { return field.Attributes.HasFlag(FieldAttributes.InitOnly); }

            // IsReadOnlyAttribute is private. thus, the hackiest hack on the planet of hacks
            foreach(var attr in member.GetCustomAttributes(false))
            {
                if(attr.GetType().FullName == "System.Runtime.CompilerServices.IsReadOnlyAttribute")
                { return true; }
            }

            return false;
        }

        (Type Type, IReadOnlyList<int> Arrays) FindRootElementType(Type type)
        {
            Assert.Ref(type);

            List<int> arrayDim = new List<int>();
            while(type.IsByRef)
            { type = type.GetElementType(); }
            
            while(type.IsArray)
            {
                arrayDim.Add(type.GetArrayRank());
                type = type.GetElementType();
            }

            return (type, arrayDim.ToArray());
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
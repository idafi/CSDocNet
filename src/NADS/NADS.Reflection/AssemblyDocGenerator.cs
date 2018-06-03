using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Collections;

namespace NADS.Reflection
{
    public class AssemblyDocGenerator : IAssemblyDocGenerator
    {
        readonly CommentIDGenerator idGen;
        
        public AssemblyDocGenerator()
        {
            idGen = new CommentIDGenerator();
        }

        public AssemblyDoc GenerateAssemblyDoc(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        public ClassDoc GenerateClassDoc(Type classType)
        {
            Check.Ref(classType);
            Check.Cond(classType.IsClass, $"type '{classType.Name}' is not a class");

            string name = classType.FullName;
            string id = idGen.GenerateTypeID(classType);

            AccessModifier access = GetTypeAccess(classType);
            Modifier modifiers = GetTypeModifiers(classType);

            IReadOnlyList<MemberRef> attributes = GetAttributes(classType);
            IReadOnlyList<TypeParam> typeParams = GetTypeParams(classType);

            MemberDoc member = new MemberDoc(name, id, access, modifiers, attributes, typeParams);
            
            return default;
        }

        public ClassDoc GenerateStructDoc(Type structType)
        {
            throw new NotImplementedException();
        }
        
        public InterfaceDoc GenerateInterfaceDoc(Type interfaceType)
        {
            throw new NotImplementedException();
        }

        public EnumDoc GenerateEnumDoc(Type enumType)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateDelegateDoc(Type delegateType)
        {
            throw new NotImplementedException();
        }

        public MemberDoc GenerateEventDoc(EventInfo eventInfo)
        {
            throw new NotImplementedException();
        }

        public FieldDoc GenerateFieldDoc(FieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        public PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateConstructorDoc(ConstructorInfo ctorInfo)
        {
            throw new NotImplementedException();
        }

        public OperatorDoc GenerateOperatorDoc(MethodInfo opInfo)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateMethodDoc(MethodInfo methodInfo)
        {
            throw new NotImplementedException();
        }

        public AccessModifier GetTypeAccess(Type type)
        {
            if(type.IsPublic || type.IsNestedPublic)
            { return AccessModifier.Public; }
            else if(!type.IsNested || type.IsNestedAssembly)
            { return AccessModifier.Internal; }
            else if(type.IsNestedFamORAssem)
            { return AccessModifier.ProtectedInternal; }
            else if(type.IsNestedFamily)
            { return AccessModifier.Protected; }
            else if(type.IsNestedFamANDAssem)
            { return AccessModifier.PrivateProtected; }
            else if(type.IsNestedPrivate)
            { return AccessModifier.Private; }
            else
            { throw new NotSupportedException($"Unknown access modifier on type '{type.Name}'"); }
        }

        public Modifier GetTypeModifiers(Type type)
        {
            Modifier mod = Modifier.None;

            if(type.IsAbstract && type.IsSealed)
            { mod |= Modifier.Static; }
            else if(type.IsAbstract)
            { mod |= Modifier.Abstract; }
            else if(type.IsSealed && !type.IsValueType)
            { mod |= Modifier.Sealed; }

            // IsReadOnlyAttribute is private; thus, the hackiest hack on the planet of hacks
            string readonlyAttrName = "System.Runtime.CompilerServices.IsReadOnlyAttribute";
            foreach(var attr in type.CustomAttributes)
            {
                if(attr.AttributeType.FullName == readonlyAttrName)
                {
                    mod |= Modifier.Readonly;
                    break;
                }
            }

            return mod;
        }

        public IReadOnlyList<MemberRef> GetAttributes(MemberInfo memberInfo)
        {
            List<MemberRef> attributes = new List<MemberRef>();
            foreach(var attr in memberInfo.CustomAttributes)
            {
                MemberRefType attrRefType = GetMemberRefType(attr.AttributeType);
                string attrName = attr.AttributeType.FullName;

                attributes.Add(new MemberRef(attrRefType, attrName));
            }

            return attributes.ToArray();
        }

        public IReadOnlyList<TypeParam> GetTypeParams(Type type)
        {
            List<TypeParam> typeParams = new List<TypeParam>();
            foreach(var typeParam in type.GetGenericArguments())
            {
                string name = typeParam.Name;
                ParamModifier modifier = GetGenericParamModifier(typeParam.GenericParameterAttributes);
                IReadOnlyList<TypeConstraint> constraints = GetTypeParamConstraints(typeParam);

                typeParams.Add(new TypeParam(name, modifier, constraints));
            }

            return typeParams.ToArray();
        }

        public MemberRefType GetMemberRefType(Type type)
        {
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
            { throw new NotSupportedException($"Couldn't determine member type of type '{type.Name}'"); }
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
    }
}
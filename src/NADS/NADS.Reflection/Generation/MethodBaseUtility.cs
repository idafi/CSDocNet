using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class MethodBaseUtility : IMethodBaseUtility
    {
        readonly IDocGeneratorUtility utility;
        readonly ICommentIDGenerator idGen;

        public MethodBaseUtility(IDocGeneratorUtility utility, ICommentIDGenerator idGen)
        {
            Check.Ref(utility, idGen);

            this.utility = utility;
            this.idGen = idGen;
        }
        
        public MemberDoc GenerateMemberDoc(MethodBase member)
        {
            Check.Ref(member);

            return new MemberDoc(
                GenerateName(member),
                GenerateCommentID(member),
                GenerateAccess(member),
                GenerateModifiers(member),
                GenerateAttributes(member)
            );
        }

        public IReadOnlyList<Param> GenerateParams(MethodBase methodInfo)
        {
            Check.Ref(methodInfo);

            ParameterInfo[] paramInfo = methodInfo.GetParameters();
            Param[] parameters = new Param[paramInfo.Length];

            for(int i = 0; i < paramInfo.Length; i++)
            { parameters[i] = GenerateParam(paramInfo[i]); }

            return parameters;
        }

        public IReadOnlyList<TypeParam> GenerateTypeParams(MethodBase methodInfo)
        {
            Check.Ref(methodInfo);

            if(methodInfo.ContainsGenericParameters)
            {
                Type[] typeArgs = methodInfo.GetGenericArguments();
                TypeParam[] typeParams = new TypeParam[typeArgs.Length];
                
                for(int i = 0; i < typeArgs.Length; i++)
                {
                    ParamModifier modifier = utility.GetGenericParamModifier(typeArgs[i].GenericParameterAttributes);
                    IReadOnlyList<TypeConstraint> constraints = utility.GetTypeParamConstraints(typeArgs[i]);
                    typeParams[i] = new TypeParam(typeArgs[i].Name, modifier, constraints);
                }

                return typeParams;
            }

            return Empty<TypeParam>.List;
        }

        public string GenerateName(MethodBase member)
        {
            Check.Ref(member);

            return utility.GenerateName(member);
        }

        public string GenerateCommentID(MethodBase member)
        {
            Check.Ref(member);

            return idGen.GenerateMemberID(member);
        }
        
        public AccessModifier GenerateAccess(MethodBase member)
        {
            Check.Ref(member);

            MethodAttributes attr = member.Attributes & MethodAttributes.MemberAccessMask;
            switch(attr)
            {
                case MethodAttributes.Public:
                    return AccessModifier.Public;
                case MethodAttributes.FamORAssem:
                    return AccessModifier.ProtectedInternal;
                case MethodAttributes.Assembly:
                    return AccessModifier.Internal;
                case MethodAttributes.Family:
                    return AccessModifier.Protected;
                case MethodAttributes.FamANDAssem:
                    return AccessModifier.PrivateProtected;
                case MethodAttributes.Private:
                    return AccessModifier.Private;
                default:
                    throw new NotSupportedException($"unrecognized access type on method '{utility.GenerateName(member)}'");
            }
        }

        public Modifier GenerateModifiers(MethodBase member)
        {
            Check.Ref(member);

            Modifier mod = Modifier.None;

            if(member.IsAbstract)
            { mod |= Modifier.Abstract; }
            else if(member is MethodInfo m && m.GetBaseDefinition().DeclaringType != m.DeclaringType)
            { mod |= Modifier.Override; }
            else if(member.IsVirtual)
            { mod |= Modifier.Virtual; }

            if(member.Attributes.HasFlag(MethodAttributes.PinvokeImpl))
            { mod |= Modifier.Extern; }
            if(member.Attributes.HasFlag(MethodAttributes.Final))
            { mod |= Modifier.Sealed; }
            
            if(member.IsStatic)
            { mod |= Modifier.Static; }

            if(member.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
            { mod |= Modifier.Async; }
            
            return mod;
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(MethodBase member)
        {
            Check.Ref(member);

            return utility.GenerateAttributes(member);
        }

        Param GenerateParam(ParameterInfo parameterInfo)
        {
            Assert.Ref(parameterInfo);

            ParamModifier modifier = GenerateParamModifier(parameterInfo);
            MemberRef type = utility.MakeMemberRef(parameterInfo.ParameterType);

            bool isGenericType = parameterInfo.ParameterType.IsGenericParameter;
            int genericPos = (isGenericType) ? parameterInfo.ParameterType.GenericParameterPosition : -1;

            bool hasDefaultValue = parameterInfo.HasDefaultValue;
            object defaultValue = (hasDefaultValue) ? parameterInfo.RawDefaultValue : null;

            return new Param(modifier, type, isGenericType, genericPos, hasDefaultValue, defaultValue);
        }

        ParamModifier GenerateParamModifier(ParameterInfo parameterInfo)
        {
            Assert.Ref(parameterInfo);

            if(parameterInfo.ParameterType.IsByRef)
            { 
                switch(parameterInfo.Attributes)
                {
                    case ParameterAttributes.In:
                        return ParamModifier.In;
                    case ParameterAttributes.Out:
                        return ParamModifier.Out;
                    default:
                        return ParamModifier.Ref;
                }
            }

            return ParamModifier.None;
        }
    }
}
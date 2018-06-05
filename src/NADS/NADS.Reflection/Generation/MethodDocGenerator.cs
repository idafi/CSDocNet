using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class MethodDocGenerator : IMethodDocGenerator
    {
        readonly IDocGeneratorUtility utility;
        readonly ICommentIDGenerator idGen;

        public MethodDocGenerator(IDocGeneratorUtility utility, ICommentIDGenerator idGen)
        {
            Check.Ref(utility, idGen);

            this.utility = utility;
            this.idGen = idGen;
        }

        public MethodDoc GenerateMethodDoc(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return new MethodDoc(
                GenerateMemberDoc(methodInfo),
                GenerateReturnType(methodInfo),
                GenerateParams(methodInfo)
            );
        }

        public MemberDoc GenerateMemberDoc(MethodInfo member)
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

        public MemberRef GenerateReturnType(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return utility.MakeMemberRef(methodInfo.ReturnType);
        }

        public IReadOnlyList<Param> GenerateParams(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            ParameterInfo[] paramInfo = methodInfo.GetParameters();
            Param[] parameters = new Param[paramInfo.Length];

            for(int i = 0; i < paramInfo.Length; i++)
            { parameters[i] = GenerateParam(paramInfo[i]); }

            return parameters;
        }

        public string GenerateName(MethodInfo member)
        {
            Check.Ref(member);

            return utility.GenerateName(member);
        }

        public string GenerateCommentID(MethodInfo member)
        {
            Check.Ref(member);

            return idGen.GenerateMethodID(member);
        }
        
        public AccessModifier GenerateAccess(MethodInfo member)
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

        public Modifier GenerateModifiers(MethodInfo member)
        {
            Check.Ref(member);

            Modifier mod = Modifier.None;

            if(member.IsAbstract)
            { mod |= Modifier.Abstract; }
            else if(member.GetBaseDefinition().DeclaringType != member.DeclaringType)
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
        
        public IReadOnlyList<MemberRef> GenerateAttributes(MethodInfo member)
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
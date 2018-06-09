using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class MethodDocGenerator : IMethodDocGenerator
    {
        readonly IDocGeneratorUtility docUtility;
        readonly IMethodBaseUtility methodUtility;
        readonly ICommentIDGenerator idGen;

        public MethodDocGenerator(IDocGeneratorUtility docUtility, IMethodBaseUtility methodUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(docUtility, methodUtility, idGen);

            this.docUtility = docUtility;
            this.methodUtility = methodUtility;
            this.idGen = idGen;
        }

        public MethodDoc GenerateMethodDoc(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return new MethodDoc(
                GenerateMemberDoc(methodInfo),
                GenerateReturnValue(methodInfo),
                GenerateParams(methodInfo),
                GenerateTypeParams(methodInfo)
            );
        }

        public MemberDoc GenerateMemberDoc(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateMemberDoc(member);
        }

        public ReturnValue GenerateReturnValue(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            Type returnType = methodInfo.ReturnType;
            ReturnModifier modifier = GenerateReturnModifier(methodInfo.ReturnParameter);

            if(returnType.IsGenericParameter)
            {
                int genericPos = returnType.GenericParameterPosition;
                return new ReturnValue(modifier, default, true, genericPos);
            }
            
            MemberRef mRef = docUtility.MakeMemberRef(returnType);
            return new ReturnValue(modifier, mRef, false, -1);
        }

        public IReadOnlyList<Param> GenerateParams(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return methodUtility.GenerateParams(methodInfo);
        }

        public IReadOnlyList<TypeParam> GenerateTypeParams(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return methodUtility.GenerateTypeParams(methodInfo);
        }

        public string GenerateName(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateName(member);
        }

        public string GenerateCommentID(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateCommentID(member);
        }
        
        public AccessModifier GenerateAccess(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateAccess(member);
        }

        public Modifier GenerateModifiers(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateModifiers(member);
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateAttributes(member);
        }

        ReturnModifier GenerateReturnModifier(ParameterInfo returnParam)
        {
            if(returnParam.ParameterType.IsByRef)
            {
                if(docUtility.IsReadOnly(returnParam))
                { return ReturnModifier.RefReadonly; }

                return ReturnModifier.Ref;
            }

            return ReturnModifier.None;
        }
    }
}
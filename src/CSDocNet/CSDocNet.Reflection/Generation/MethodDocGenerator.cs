using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CSDocNet.Collections;
using CSDocNet.Debug;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class MethodDataGenerator : IMethodDataGenerator
    {
        readonly IDataGeneratorUtility docUtility;
        readonly IMethodBaseUtility methodUtility;

        public MethodDataGenerator(IDataGeneratorUtility docUtility, IMethodBaseUtility methodUtility)
        {
            Check.Ref(docUtility, methodUtility);

            this.docUtility = docUtility;
            this.methodUtility = methodUtility;
        }

        public MethodData GenerateMethodData(MethodInfo methodInfo)
        {
            Check.Ref(methodInfo);

            return new MethodData(
                GenerateMemberData(methodInfo),
                GenerateReturnValue(methodInfo),
                methodUtility.GenerateParams(methodInfo),
                methodUtility.GenerateTypeParams(methodInfo)
            );
        }

        public MemberData GenerateMemberData(MethodInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateMemberData(member);
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
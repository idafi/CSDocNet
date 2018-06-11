using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class OperatorDataGenerator : IOperatorDataGenerator
    {
        readonly IMethodDataGenerator methodGen;
        
        public OperatorDataGenerator(IMethodDataGenerator methodGen)
        {
            Check.Ref(methodGen);

            this.methodGen = methodGen;
        }

        public OperatorData GenerateOperatorData(MethodInfo opInfo)
        {
            Check.Ref(opInfo);

            return new OperatorData(
                GenerateMemberData(opInfo),
                methodGen.GenerateMethodData(opInfo),
                GenerateOperator(opInfo)
            );
        }

        public MemberData GenerateMemberData(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateMemberData(member);
        }

        public string GenerateName(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateName(member);
        }

        public string GenerateCommentID(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateCommentID(member);
        }
        
        public AccessModifier GenerateAccess(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateAccess(member);
        }

        public Modifier GenerateModifiers(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateModifiers(member);
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(MethodInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateAttributes(member);
        }

        public Operator GenerateOperator(MethodInfo opInfo)
        {
            Check.Ref(opInfo);
            Check.Cond(opInfo.IsSpecialName && opInfo.Name.StartsWith("op_"),
                "method info must represent an operator");
            
            string opName = opInfo.Name.Substring(3);
            if(!Enum.TryParse(opName, out Operator result))
            { throw new NotSupportedException($"unknown operator '{opName}'"); }

            return result;
        }
    }
}
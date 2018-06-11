using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class ConstructorDataGenerator : IConstructorDataGenerator
    {
        readonly IMethodBaseUtility methodUtility;
        
        public ConstructorDataGenerator(IMethodBaseUtility methodUtility)
        {
            Check.Ref(methodUtility);

            this.methodUtility = methodUtility;
        }

        public MethodData GenerateConstructorData(ConstructorInfo ctorInfo)
        {
            Check.Ref(ctorInfo);

            return new MethodData(
                GenerateMemberData(ctorInfo),
                default,
                methodUtility.GenerateParams(ctorInfo),
                methodUtility.GenerateTypeParams(ctorInfo)
            );
        }

        public MemberData GenerateMemberData(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateMemberData(member);
        }

        public string GenerateName(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateName(member);
        }

        public string GenerateCommentID(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateCommentID(member);
        }
        
        public AccessModifier GenerateAccess(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateAccess(member);
        }

        public Modifier GenerateModifiers(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateModifiers(member);
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateAttributes(member);
        }
    }
}
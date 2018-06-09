using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using NADS.Collections;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class ConstructorDocGenerator : IConstructorDocGenerator
    {
        readonly IMethodBaseUtility methodUtility;
        
        public ConstructorDocGenerator(IMethodBaseUtility methodUtility)
        {
            Check.Ref(methodUtility);

            this.methodUtility = methodUtility;
        }

        public MethodDoc GenerateConstructorDoc(ConstructorInfo ctorInfo)
        {
            Check.Ref(ctorInfo);

            return new MethodDoc(
                GenerateMemberDoc(ctorInfo),
                default,
                methodUtility.GenerateParams(ctorInfo),
                methodUtility.GenerateTypeParams(ctorInfo)
            );
        }

        public MemberDoc GenerateMemberDoc(ConstructorInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateMemberDoc(member);
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
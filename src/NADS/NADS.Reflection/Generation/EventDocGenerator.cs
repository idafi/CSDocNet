using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class EventDocGenerator : IEventDocGenerator
    {
        readonly IDocGeneratorUtility docUtility;
        readonly IMethodBaseUtility methodUtility;
        readonly ICommentIDGenerator idGen;

        public EventDocGenerator(IDocGeneratorUtility utility, IMethodBaseUtility methodUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(utility, methodUtility, idGen);

            this.docUtility = utility;
            this.methodUtility = methodUtility;
            this.idGen = idGen;
        }

        public MemberDoc GenerateMemberDoc(EventInfo member)
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

        public string GenerateName(EventInfo member)
        {
            Check.Ref(member);

            return docUtility.GenerateName(member);
        }

        public string GenerateCommentID(EventInfo member)
        {
            Check.Ref(member);

            return idGen.GenerateMemberID(member);
        }
        
        public AccessModifier GenerateAccess(EventInfo member)
        {
            Check.Ref(member);

            return methodUtility.GenerateAccess(member.AddMethod);
        }

        public Modifier GenerateModifiers(EventInfo member)
        {
            Check.Ref(member);

            return Modifier.None;
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(EventInfo member)
        {
            Check.Ref(member);

            return docUtility.GenerateAttributes(member);
        }
    }
}
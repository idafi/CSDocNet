using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class EventDocGenerator : IEventDocGenerator
    {
        readonly IDocGeneratorUtility utility;
        readonly ICommentIDGenerator idGen;
        readonly IMethodDocGenerator methodGen;

        public EventDocGenerator(IDocGeneratorUtility utility, ICommentIDGenerator idGen,
            IMethodDocGenerator methodGen)
        {
            Check.Ref(utility, idGen, methodGen);

            this.utility = utility;
            this.idGen = idGen;
            this.methodGen = methodGen;
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

            return utility.GenerateName(member);
        }

        public string GenerateCommentID(EventInfo member)
        {
            Check.Ref(member);

            return idGen.GenerateEventID(member);
        }
        
        public AccessModifier GenerateAccess(EventInfo member)
        {
            Check.Ref(member);

            return methodGen.GenerateAccess(member.AddMethod);
        }

        public Modifier GenerateModifiers(EventInfo member)
        {
            Check.Ref(member);

            return Modifier.None;
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(EventInfo member)
        {
            Check.Ref(member);

            return utility.GenerateAttributes(member);
        }
    }
}
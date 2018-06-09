using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class PropertyDocGenerator : IPropertyDocGenerator
    {
        readonly IDocGeneratorUtility docUtility;
        readonly IMethodBaseUtility methodUtility;
        readonly ICommentIDGenerator idGen;

        public PropertyDocGenerator(IDocGeneratorUtility utility, IMethodBaseUtility methodUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(utility, idGen, methodUtility);

            this.docUtility = utility;
            this.idGen = idGen;
            this.methodUtility = methodUtility;
        }

        public PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo)
        {
            Check.Ref(propertyInfo);

            var get = (propertyInfo.CanRead)
                ? GenerateAccessor(propertyInfo.GetMethod)
                : new PropertyDoc.Accessor(false, default);
            var set = (propertyInfo.CanWrite)
                ? GenerateAccessor(propertyInfo.SetMethod)
                : new PropertyDoc.Accessor(false, default);
            
            return new PropertyDoc(GenerateMemberDoc(propertyInfo), get, set);
        }

        public MemberDoc GenerateMemberDoc(PropertyInfo member)
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

        public PropertyDoc.Accessor GenerateAccessor(MethodInfo accessorInfo)
        {
            Check.Ref(accessorInfo);

            return new PropertyDoc.Accessor(true, methodUtility.GenerateAccess(accessorInfo));
        }

        public string GenerateName(PropertyInfo member)
        {
            Check.Ref(member);

            return docUtility.GenerateName(member);
        }

        public string GenerateCommentID(PropertyInfo member)
        {
            Check.Ref(member);

            return idGen.GenerateMemberID(member);
        }
        
        public AccessModifier GenerateAccess(PropertyInfo member)
        {
            Check.Ref(member);

            AccessModifier get = AccessModifier.Private;
            AccessModifier set = AccessModifier.Private;

            if(member.CanRead)
            { get = methodUtility.GenerateAccess(member.GetMethod); }
            if(member.CanWrite)
            { set = methodUtility.GenerateAccess(member.SetMethod); }

            return (AccessModifier)(Math.Max((int)(get), (int)(set)));
        }

        public Modifier GenerateModifiers(PropertyInfo member)
        {
            Check.Ref(member);

            if(member.CanRead)
            { return methodUtility.GenerateModifiers(member.GetMethod); }
            else if(member.CanWrite)
            { return methodUtility.GenerateModifiers(member.SetMethod); }
            else
            { throw new NotSupportedException($"Property '{member.Name}' has neither get nor set accessor"); }
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(PropertyInfo member)
        {
            Check.Ref(member);

            return docUtility.GenerateAttributes(member);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class EnumDocGenerator : IEnumDocGenerator
    {
        readonly IDocGeneratorUtility docUtility;
        readonly ITypeDocUtility typeUtility;
        readonly ICommentIDGenerator idGen;

        public EnumDocGenerator(IDocGeneratorUtility docUtility, ITypeDocUtility typeUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(docUtility, typeUtility,  idGen);

            this.docUtility = docUtility;
            this.typeUtility = typeUtility;
            this.idGen = idGen;
        }

        public EnumDoc GenerateEnumDoc(Type type)
        {
            Check.Ref(type);
            Check.Cond(type.IsEnum, "type must be an enum");

            return new EnumDoc(
                GenerateMemberDoc(type),
                GenerateUnderlyingType(type),
                GenerateEnumValues(type)
            );
        }

        public MemberDoc GenerateMemberDoc(Type member)
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

        public string GenerateName(Type member)
        {
            Check.Ref(member);

            return docUtility.GenerateName(member);
        }

        public string GenerateCommentID(Type member)
        {
            Check.Ref(member);

            return idGen.GenerateMemberID(member);
        }
        
        public AccessModifier GenerateAccess(Type member)
        {
            Check.Ref(member);

            return typeUtility.GenerateAccess(member);
        }

        public Modifier GenerateModifiers(Type member)
        {
            Check.Ref(member);

            return typeUtility.GenerateModifiers(member);
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(Type member)
        {
            Check.Ref(member);

            return docUtility.GenerateAttributes(member);
        }

        public MemberRef GenerateUnderlyingType(Type type)
        {
            Check.Ref(type);
            Check.Cond(type.IsEnum, "type must be an enum");

            return docUtility.MakeMemberRef(type.GetEnumUnderlyingType());
        }

        public IReadOnlyList<EnumValue> GenerateEnumValues(Type type)
        {
            Check.Ref(type);
            Check.Cond(type.IsEnum, "type must be an enum");

            string[] names = type.GetEnumNames();
            Array values = type.GetEnumValues();
            EnumValue[] enumValues = new EnumValue[names.Length];

            for(int i = 0; i < names.Length; i++)
            { enumValues[i] = new EnumValue(names[i], Convert.ToInt64(values.GetValue(i))); }

            return enumValues;
        }
    }
}
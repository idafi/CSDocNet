using System;
using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class EnumDataGenerator : IEnumDataGenerator
    {
        readonly IDataGeneratorUtility docUtility;
        readonly ITypeDataUtility typeUtility;
        readonly ICommentIDGenerator idGen;

        public EnumDataGenerator(IDataGeneratorUtility docUtility, ITypeDataUtility typeUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(docUtility, typeUtility,  idGen);

            this.docUtility = docUtility;
            this.typeUtility = typeUtility;
            this.idGen = idGen;
        }

        public EnumData GenerateEnumData(Type type)
        {
            Check.Ref(type);
            Check.Cond(type.IsEnum, "type must be an enum");

            return new EnumData(
                GenerateMemberData(type),
                GenerateUnderlyingType(type),
                GenerateEnumValues(type)
            );
        }

        public MemberData GenerateMemberData(Type member)
        {
            Check.Ref(member);

            return new MemberData(
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
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class FieldDataGenerator : IFieldDataGenerator
    {
        readonly IDataGeneratorUtility utility;
        readonly ICommentIDGenerator idGen;

        public FieldDataGenerator(IDataGeneratorUtility utility, ICommentIDGenerator idGen)
        {
            Check.Ref(utility, idGen);

            this.utility = utility;
            this.idGen = idGen;
        }

        public FieldData GenerateFieldData(FieldInfo fieldInfo)
        {
            Check.Ref(fieldInfo);

            return new FieldData(GenerateMemberData(fieldInfo), GenerateConstValue(fieldInfo));
        }

        public MemberData GenerateMemberData(FieldInfo member)
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

        public object GenerateConstValue(FieldInfo fieldInfo)
        {
            Check.Ref(fieldInfo);

            if(fieldInfo.IsLiteral)
            { return fieldInfo.GetRawConstantValue(); }

            return null;
        }

        public string GenerateName(FieldInfo member)
        {
            Check.Ref(member);

            return utility.GenerateName(member);
        }

        public string GenerateCommentID(FieldInfo member)
        {
            Check.Ref(member);

            return idGen.GenerateMemberID(member);
        }
        
        public AccessModifier GenerateAccess(FieldInfo member)
        {
            Check.Ref(member);

            FieldAttributes attr = member.Attributes & FieldAttributes.FieldAccessMask;
            switch(attr)
            {
                case FieldAttributes.Public:
                    return AccessModifier.Public;
                case FieldAttributes.FamORAssem:
                    return AccessModifier.ProtectedInternal;
                case FieldAttributes.Assembly:
                    return AccessModifier.Internal;
                case FieldAttributes.Family:
                    return AccessModifier.Protected;
                case FieldAttributes.FamANDAssem:
                    return AccessModifier.PrivateProtected;
                case FieldAttributes.Private:
                    return AccessModifier.Private;
                default:
                    throw new NotSupportedException($"unrecognized access type on field '{utility.GenerateName(member)}'");
            }
        }

        public Modifier GenerateModifiers(FieldInfo member)
        {
            Check.Ref(member);

            Modifier mod = Modifier.None;

            if(member.IsLiteral)
            { mod |= Modifier.Const; }
            else if(member.IsStatic)
            { mod |= Modifier.Static; }

            if(member.IsInitOnly)
            { mod |= Modifier.Readonly; }
            if(Array.FindIndex(member.GetRequiredCustomModifiers(), t => t == typeof(IsVolatile)) > -1)
            { mod |= Modifier.Volatile; }

            return mod;
        }
        
        public IReadOnlyList<MemberRef> GenerateAttributes(FieldInfo member)
        {
            Check.Ref(member);

            return utility.GenerateAttributes(member);
        }
    }
}
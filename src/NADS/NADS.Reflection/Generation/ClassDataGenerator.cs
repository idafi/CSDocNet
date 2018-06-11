using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Debug;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class ClassDataGenerator : IClassDataGenerator
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static
                | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly;

        readonly IDataGeneratorUtility docUtility;
        readonly ITypeDataUtility typeUtility;
        readonly ICommentIDGenerator idGen;

        public ClassDataGenerator(IDataGeneratorUtility docUtility, ITypeDataUtility typeUtility,
            ICommentIDGenerator idGen)
        {
            Check.Ref(docUtility, typeUtility,  idGen);

            this.docUtility = docUtility;
            this.typeUtility = typeUtility;
            this.idGen = idGen;
        }

        public ClassData GenerateClassData(Type type)
        {
            Check.Ref(type);

            ClassDataMethods methods = GenerateClassDataMethods(type);

            return new ClassData(
                GenerateMemberData(type),
                GenerateInheritsRef(type),
                GenerateImplementsList(type),
                GenerateEvents(type),
                GenerateFields(type),
                GenerateProperties(type),
                GenerateConstructors(type),
                methods.Operators,
                methods.Methods
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

        public MemberRef GenerateInheritsRef(Type type)
        {
            Check.Ref(type);

            return docUtility.MakeMemberRef(type.BaseType ?? typeof(object));
        }

        public IReadOnlyList<MemberRef> GenerateImplementsList(Type type)
        {
            Check.Ref(type);

            return GenerateMemberRefs(type.GetInterfaces());
        }

        public IReadOnlyList<MemberRef> GenerateEvents(Type type)
        {
            Check.Ref(type);

            return GenerateMemberRefs(type.GetEvents(bindingFlags));
        }

        public IReadOnlyList<MemberRef> GenerateFields(Type type)
        {
            Check.Ref(type);

            return GenerateMemberRefs(type.GetFields(bindingFlags));
        }

        public IReadOnlyList<MemberRef> GenerateProperties(Type type)
        {
            Check.Ref(type);

            return GenerateMemberRefs(type.GetProperties(bindingFlags));
        }

        public IReadOnlyList<MemberRef> GenerateConstructors(Type type)
        {
            Check.Ref(type);

            return GenerateMemberRefs(type.GetConstructors(bindingFlags));
        }

        public ClassDataMethods GenerateClassDataMethods(Type type)
        {
            Check.Ref(type);

            MethodInfo[] methodInfo = type.GetMethods(bindingFlags);
            List<MemberRef> methods = new List<MemberRef>(methodInfo.Length);
            List<MemberRef> operators = new List<MemberRef>(30);

            foreach(MethodInfo m in methodInfo)
            {
                if(m.IsSpecialName)
                {
                    if(m.Name.StartsWith("op_"))
                    { operators.Add(docUtility.MakeMemberRef(m)); }
                }
                else
                { methods.Add(docUtility.MakeMemberRef(m)); }
            }

            return new ClassDataMethods(methods.ToArray(), operators.ToArray());
        }

        IReadOnlyList<MemberRef> GenerateMemberRefs(MemberInfo[] members)
        {
            Assert.Ref(members);

            MemberRef[] refs = new MemberRef[members.Length];
            for(int i = 0; i < members.Length; i++)
            { refs[i] = docUtility.MakeMemberRef(members[i]); }

            return refs;
        }
    }
}
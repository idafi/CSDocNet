using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IClassDataGenerator : IMemberDataGenerator<Type>
    {
        ClassData GenerateClassData(Type type);

        MemberRef GenerateInheritsRef(Type type);
        IReadOnlyList<MemberRef> GenerateImplementsList(Type type);

        IReadOnlyList<MemberRef> GenerateEvents(Type type);
        IReadOnlyList<MemberRef> GenerateFields(Type type);
        IReadOnlyList<MemberRef> GenerateProperties(Type type);
        IReadOnlyList<MemberRef> GenerateConstructors(Type type);
        ClassDataMethods GenerateClassDataMethods(Type type);
    }
}
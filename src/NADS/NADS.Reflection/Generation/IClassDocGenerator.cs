using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IClassDocGenerator : IMemberDocGenerator<Type>
    {
        ClassDoc GenerateClassDoc(Type type);

        MemberRef GenerateInheritsRef(Type type);
        IReadOnlyList<MemberRef> GenerateImplementsList(Type type);

        IReadOnlyList<MemberRef> GenerateEvents(Type type);
        IReadOnlyList<MemberRef> GenerateFields(Type type);
        IReadOnlyList<MemberRef> GenerateProperties(Type type);
        IReadOnlyList<MemberRef> GenerateConstructors(Type type);
        ClassDocMethods GenerateClassDocMethods(Type type);
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IEnumDataGenerator : IMemberDataGenerator<Type>
    {
        EnumData GenerateEnumData(Type type);

        MemberRef GenerateUnderlyingType(Type type);
        IReadOnlyList<EnumValue> GenerateEnumValues(Type type);
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface IEnumDataGenerator : IMemberDataGenerator<Type>
    {
        EnumData GenerateEnumData(Type type);

        MemberRef GenerateUnderlyingType(Type type);
        IReadOnlyList<EnumValue> GenerateEnumValues(Type type);
    }
}
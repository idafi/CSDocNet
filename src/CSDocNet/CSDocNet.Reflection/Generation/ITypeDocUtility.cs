using System;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public interface ITypeDataUtility
    {
        AccessModifier GenerateAccess(Type type);
        Modifier GenerateModifiers(Type type);
    }
}
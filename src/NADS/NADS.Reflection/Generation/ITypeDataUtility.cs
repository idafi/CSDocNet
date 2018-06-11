using System;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface ITypeDataUtility
    {
        AccessModifier GenerateAccess(Type type);
        Modifier GenerateModifiers(Type type);
    }
}
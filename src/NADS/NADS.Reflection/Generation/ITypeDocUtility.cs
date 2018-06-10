using System;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface ITypeDocUtility
    {
        AccessModifier GenerateAccess(Type type);
        Modifier GenerateModifiers(Type type);
    }
}
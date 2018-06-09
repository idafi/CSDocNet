using System;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface ICommentIDGenerator
    {
        string GenerateMemberID(Type type);
        string GenerateMemberID(FieldInfo field);
        string GenerateMemberID(PropertyInfo property);
        string GenerateMemberID(MethodBase method);
        string GenerateMemberID(MethodInfo method);
        string GenerateMemberID(ConstructorInfo ctor);
        string GenerateMemberID(EventInfo ev);
    }
}
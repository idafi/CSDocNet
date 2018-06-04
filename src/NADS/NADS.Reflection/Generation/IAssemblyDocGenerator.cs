using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public interface IAssemblyDocGenerator
    {
        AssemblyDoc GenerateAssemblyDoc(Assembly assembly);

        ClassDoc GenerateClassDoc(Type classType);
        ClassDoc GenerateStructDoc(Type structType);
        InterfaceDoc GenerateInterfaceDoc(Type interfaceType);
        EnumDoc GenerateEnumDoc(Type enumType);
        MethodDoc GenerateDelegateDoc(Type delegateType);

        MemberDoc GenerateEventDoc(EventInfo eventInfo);
        FieldDoc GenerateFieldDoc(FieldInfo fieldInfo);
        PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo);
        MethodDoc GenerateConstructorDoc(ConstructorInfo ctorInfo);
        OperatorDoc GenerateOperatorDoc(MethodInfo opInfo);
        MethodDoc GenerateMethodDoc(MethodInfo methodInfo);
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NADS.Reflection
{
    public class AssemblyDocGenerator : IAssemblyDocGenerator
    {
        public AssemblyDoc GenerateAssemblyDoc(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        public ClassDoc GenerateClassDoc(Type classType)
        {
            throw new NotImplementedException();
        }

        public ClassDoc GenerateStructDoc(Type structType)
        {
            throw new NotImplementedException();
        }
        
        public InterfaceDoc GenerateInterfaceDoc(Type interfaceType)
        {
            throw new NotImplementedException();
        }

        public EnumDoc GenerateEnumDoc(Type enumType)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateDelegateDoc(Type delegateType)
        {
            throw new NotImplementedException();
        }

        public MemberDoc GenerateEventDoc(EventInfo eventInfo)
        {
            throw new NotImplementedException();
        }

        public FieldDoc GenerateFieldDoc(FieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }

        public PropertyDoc GeneratePropertyDoc(PropertyInfo propertyInfo)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateConstructorDoc(ConstructorInfo ctorInfo)
        {
            throw new NotImplementedException();
        }

        public OperatorDoc GenerateOperatorDoc(MethodInfo opInfo)
        {
            throw new NotImplementedException();
        }

        public MethodDoc GenerateMethodDoc(MethodInfo methodInfo)
        {
            throw new NotImplementedException();
        }
    }
}
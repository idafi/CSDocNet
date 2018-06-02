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

        public AccessModifier GetTypeAccess(Type type)
        {
            if(type.IsPublic || type.IsNestedPublic)
            { return AccessModifier.Public; }
            else if(!type.IsNested || type.IsNestedAssembly)
            { return AccessModifier.Internal; }
            else if(type.IsNestedFamORAssem)
            { return AccessModifier.ProtectedInternal; }
            else if(type.IsNestedFamily)
            { return AccessModifier.Protected; }
            else if(type.IsNestedFamANDAssem)
            { return AccessModifier.PrivateProtected; }
            else if(type.IsNestedPrivate)
            { return AccessModifier.Private; }
            else
            { throw new NotSupportedException($"Unknown access modifier on type '{type.Name}'"); }
        }

        public Modifier GetTypeModifiers(Type type)
        {
            Modifier mod = Modifier.None;

            if(type.IsAbstract && type.IsSealed)
            { mod |= Modifier.Static; }
            else if(type.IsAbstract)
            { mod |= Modifier.Abstract; }
            else if(type.IsSealed && !type.IsValueType)
            { mod |= Modifier.Sealed; }

            // IsReadOnlyAttribute is private; thus, the hackiest hack on the planet of hacks
            string readonlyAttrName = "System.Runtime.CompilerServices.IsReadOnlyAttribute";
            foreach(var attr in type.CustomAttributes)
            {
                if(attr.AttributeType.FullName == readonlyAttrName)
                {
                    mod |= Modifier.Readonly;
                    break;
                }
            }

            return mod;
        }
    }
}
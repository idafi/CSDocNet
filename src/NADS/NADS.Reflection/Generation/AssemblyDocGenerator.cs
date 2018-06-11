using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class AssemblyDocGenerator : IAssemblyDocGenerator
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static
            | BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.DeclaredOnly;

        readonly IClassDocGenerator classGen;
        readonly IEnumDocGenerator enumGen;

        readonly IEventDocGenerator eventGen;
        readonly IFieldDocGenerator fieldGen;
        readonly IPropertyDocGenerator propertyGen;
        readonly IConstructorDocGenerator ctorGen;
        readonly IOperatorDocGenerator operatorGen;
        readonly IMethodDocGenerator methodGen;

        public AssemblyDocGenerator()
        {
            var docUtility = new DocGeneratorUtility();
            var idGen = new CommentIDGenerator();
            var typeUtility = new TypeDocUtility(docUtility);
            var methodUtility = new MethodBaseUtility(docUtility, idGen);
            
            methodGen = new MethodDocGenerator(docUtility, methodUtility);
            classGen = new ClassDocGenerator(docUtility, typeUtility, idGen);
            enumGen = new EnumDocGenerator(docUtility, typeUtility, idGen);
            eventGen = new EventDocGenerator(docUtility, methodUtility, idGen);
            fieldGen = new FieldDocGenerator(docUtility, idGen);
            propertyGen = new PropertyDocGenerator(docUtility, methodUtility, idGen);
            ctorGen = new ConstructorDocGenerator(methodUtility);
            operatorGen = new OperatorDocGenerator(methodGen);
        }

        public AssemblyDocGenerator(
            IClassDocGenerator classGen,
            IEnumDocGenerator enumGen,
            IEventDocGenerator eventGen,
            IFieldDocGenerator fieldGen,
            IPropertyDocGenerator propertyGen,
            IConstructorDocGenerator ctorGen,
            IOperatorDocGenerator operatorGen,
            IMethodDocGenerator methodGen
        )
        {
            Check.Ref(classGen, enumGen, eventGen, fieldGen, propertyGen, ctorGen, operatorGen, methodGen);

            this.classGen = classGen;
            this.enumGen = enumGen;

            this.eventGen = eventGen;
            this.fieldGen = fieldGen;
            this.propertyGen = propertyGen;
            this.ctorGen = ctorGen;
            this.operatorGen = operatorGen;
            this.methodGen = methodGen;
        }

        public AssemblyDoc GenerateAssemblyDoc(Assembly assembly)
        {
            Check.Ref(assembly);

            var classes = new Dictionary<int, ClassDoc>();
            var structs = new Dictionary<int, ClassDoc>();
            var interfaces = new Dictionary<int, ClassDoc>();
            var enums = new Dictionary<int, EnumDoc>();
            var delegates = new Dictionary<int, MethodDoc>();

            var events = new Dictionary<int, MemberDoc>();
            var fields = new Dictionary<int, FieldDoc>();
            var properties = new Dictionary<int, PropertyDoc>();
            var constructors = new Dictionary<int, MethodDoc>();
            var operators = new Dictionary<int, OperatorDoc>();
            var methods = new Dictionary<int, MethodDoc>();

            foreach(Type type in assembly.GetTypes())
            {
                if(type.IsEnum)
                { enums.Add(type.MetadataToken, enumGen.GenerateEnumDoc(type)); }
                else if(type.IsValueType)
                { structs.Add(type.MetadataToken, classGen.GenerateClassDoc(type)); }
                else if(type.IsInterface)
                { interfaces.Add(type.MetadataToken, classGen.GenerateClassDoc(type)); }
                else if(type.BaseType == typeof(Delegate) || type.BaseType == typeof(MulticastDelegate))
                { delegates.Add(type.MetadataToken, methodGen.GenerateMethodDoc(type.GetMethod("Invoke"))); }
                else
                { classes.Add(type.MetadataToken, classGen.GenerateClassDoc(type)); }

                foreach(EventInfo e in type.GetEvents(bindingFlags))
                { events.Add(e.MetadataToken, eventGen.GenerateMemberDoc(e)); }
                foreach(FieldInfo f in type.GetFields(bindingFlags))
                { fields.Add(f.MetadataToken, fieldGen.GenerateFieldDoc(f)); }
                foreach(PropertyInfo p in type.GetProperties(bindingFlags))
                { properties.Add(p.MetadataToken, propertyGen.GeneratePropertyDoc(p)); }
                foreach(ConstructorInfo c in type.GetConstructors(bindingFlags))
                { constructors.Add(c.MetadataToken, ctorGen.GenerateConstructorDoc(c)); }
                foreach(MethodInfo m in type.GetMethods(bindingFlags))
                {
                    if(m.IsSpecialName)
                    {
                        if(m.Name.StartsWith("op_"))
                        { operators.Add(m.MetadataToken, operatorGen.GenerateOperatorDoc(m)); }
                    }
                    else
                    { methods.Add(m.MetadataToken, methodGen.GenerateMethodDoc(m)); }
                }
            }

            return new AssemblyDoc(classes, structs, interfaces, enums, delegates,
                    events, fields, properties, constructors, operators, methods);
        }
    }
}

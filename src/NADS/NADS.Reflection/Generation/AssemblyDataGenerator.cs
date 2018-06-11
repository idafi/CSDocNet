using System;
using System.Collections.Generic;
using System.Reflection;
using NADS.Reflection.Data;

namespace NADS.Reflection.Generation
{
    public class AssemblyDataGenerator : IAssemblyDataGenerator
    {
        const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static
            | BindingFlags.Public | BindingFlags.NonPublic
            | BindingFlags.DeclaredOnly;

        readonly IClassDataGenerator classGen;
        readonly IEnumDataGenerator enumGen;

        readonly IEventDataGenerator eventGen;
        readonly IFieldDataGenerator fieldGen;
        readonly IPropertyDataGenerator propertyGen;
        readonly IConstructorDataGenerator ctorGen;
        readonly IOperatorDataGenerator operatorGen;
        readonly IMethodDataGenerator methodGen;

        public AssemblyDataGenerator()
        {
            var docUtility = new DataGeneratorUtility();
            var idGen = new CommentIDGenerator();
            var typeUtility = new TypeDataUtility(docUtility);
            var methodUtility = new MethodBaseUtility(docUtility, idGen);
            
            methodGen = new MethodDataGenerator(docUtility, methodUtility);
            classGen = new ClassDataGenerator(docUtility, typeUtility, idGen);
            enumGen = new EnumDataGenerator(docUtility, typeUtility, idGen);
            eventGen = new EventDataGenerator(docUtility, methodUtility, idGen);
            fieldGen = new FieldDataGenerator(docUtility, idGen);
            propertyGen = new PropertyDataGenerator(docUtility, methodUtility, idGen);
            ctorGen = new ConstructorDataGenerator(methodUtility);
            operatorGen = new OperatorDataGenerator(methodGen);
        }

        public AssemblyDataGenerator(
            IClassDataGenerator classGen,
            IEnumDataGenerator enumGen,
            IEventDataGenerator eventGen,
            IFieldDataGenerator fieldGen,
            IPropertyDataGenerator propertyGen,
            IConstructorDataGenerator ctorGen,
            IOperatorDataGenerator operatorGen,
            IMethodDataGenerator methodGen
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

        public AssemblyData GenerateAssemblyData(Assembly assembly)
        {
            Check.Ref(assembly);

            var classes = new Dictionary<int, ClassData>();
            var structs = new Dictionary<int, ClassData>();
            var interfaces = new Dictionary<int, ClassData>();
            var enums = new Dictionary<int, EnumData>();
            var delegates = new Dictionary<int, MethodData>();

            var events = new Dictionary<int, MemberData>();
            var fields = new Dictionary<int, FieldData>();
            var properties = new Dictionary<int, PropertyData>();
            var constructors = new Dictionary<int, MethodData>();
            var operators = new Dictionary<int, OperatorData>();
            var methods = new Dictionary<int, MethodData>();

            foreach(Type type in assembly.GetTypes())
            {
                if(type.IsEnum)
                { enums.Add(type.MetadataToken, enumGen.GenerateEnumData(type)); }
                else if(type.IsValueType)
                { structs.Add(type.MetadataToken, classGen.GenerateClassData(type)); }
                else if(type.IsInterface)
                { interfaces.Add(type.MetadataToken, classGen.GenerateClassData(type)); }
                else if(type.BaseType == typeof(Delegate) || type.BaseType == typeof(MulticastDelegate))
                { delegates.Add(type.MetadataToken, methodGen.GenerateMethodData(type.GetMethod("Invoke"))); }
                else
                { classes.Add(type.MetadataToken, classGen.GenerateClassData(type)); }

                foreach(EventInfo e in type.GetEvents(bindingFlags))
                { events.Add(e.MetadataToken, eventGen.GenerateMemberData(e)); }
                foreach(FieldInfo f in type.GetFields(bindingFlags))
                { fields.Add(f.MetadataToken, fieldGen.GenerateFieldData(f)); }
                foreach(PropertyInfo p in type.GetProperties(bindingFlags))
                { properties.Add(p.MetadataToken, propertyGen.GeneratePropertyData(p)); }
                foreach(ConstructorInfo c in type.GetConstructors(bindingFlags))
                { constructors.Add(c.MetadataToken, ctorGen.GenerateConstructorData(c)); }
                foreach(MethodInfo m in type.GetMethods(bindingFlags))
                {
                    if(m.IsSpecialName)
                    {
                        if(m.Name.StartsWith("op_"))
                        { operators.Add(m.MetadataToken, operatorGen.GenerateOperatorData(m)); }
                    }
                    else
                    { methods.Add(m.MetadataToken, methodGen.GenerateMethodData(m)); }
                }
            }

            return new AssemblyData(classes, structs, interfaces, enums, delegates,
                    events, fields, properties, constructors, operators, methods);
        }
    }
}

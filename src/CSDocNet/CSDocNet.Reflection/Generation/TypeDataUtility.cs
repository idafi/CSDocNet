using System;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class TypeDataUtility : ITypeDataUtility
    {
        readonly IDataGeneratorUtility utility;

        public TypeDataUtility(IDataGeneratorUtility utility)
        {
            Check.Ref(utility);

            this.utility = utility;
        }

        public AccessModifier GenerateAccess(Type member)
        {
            Check.Ref(member);

            TypeAttributes attr = member.Attributes & TypeAttributes.VisibilityMask;
            switch(attr)
            {
                case TypeAttributes.Public:
                case TypeAttributes.NestedPublic:
                    return AccessModifier.Public;
                case TypeAttributes.NestedFamORAssem:
                    return AccessModifier.ProtectedInternal;
                case TypeAttributes.NestedAssembly:
                case TypeAttributes.NotPublic:
                    return AccessModifier.Internal;
                case TypeAttributes.NestedFamily:
                    return AccessModifier.Protected;
                case TypeAttributes.NestedFamANDAssem:
                    return AccessModifier.PrivateProtected;
                case TypeAttributes.NestedPrivate:
                    return AccessModifier.Private;
                default:
                    throw new NotSupportedException($"Unknown access modifier on enum '{member.Name}'");
            }
        }

        public Modifier GenerateModifiers(Type member)
        {
            Check.Ref(member);

            Modifier mod = Modifier.None;

            if(member.IsAbstract && member.IsSealed)
            { mod |= Modifier.Static; }
            else if(member.IsAbstract)
            { mod |= Modifier.Abstract; }
            else if(member.IsSealed && !member.IsValueType)
            { mod |= Modifier.Sealed; }

            if(utility.IsReadOnly(member))
            { mod |= Modifier.Readonly; }
            if(IsByRefLike(member))
            { mod |= Modifier.Ref; }

            return mod;
        }

        bool IsByRefLike(Type member)
        {
            // HACK: why is this internal, i don't get it
            foreach(var attr in member.CustomAttributes)
            {
                if(attr.AttributeType.FullName == "System.Runtime.CompilerServices.IsByRefLikeAttribute")
                { return true; }
            }

            return false;
        }
    }
}
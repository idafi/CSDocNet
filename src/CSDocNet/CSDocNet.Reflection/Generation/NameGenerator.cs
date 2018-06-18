using System;
using System.Collections.Generic;
using System.Reflection;
using CSDocNet.Reflection.Data;

namespace CSDocNet.Reflection.Generation
{
    public class NameGenerator : INameGenerator
    {
        public string GenerateMemberName(MemberInfo member)
        {
            switch(member)
            {
                case Type t:
                    return GenerateTypeName(t);
                case EventInfo e:
                    return GenerateEventName(e);
                case FieldInfo f:
                    return GenerateFieldName(f);
                case PropertyInfo p:
                    return GeneratePropertyName(p);
                case MethodBase m:
                    return GenerateMethodName(m);
                default:
                    throw new NotSupportedException($"unknown member type '{member.GetType()}'");
            }
        }

        public string GenerateTypeName(Type type)
        {
            string name = type.Name;

            if(type.IsGenericType || type.IsGenericTypeDefinition || type.IsConstructedGenericType)
            { name = name.Substring(0, name.IndexOf('`')); }

            return name;
        }

        public string GenerateEventName(EventInfo ev)
        {
            return ev.Name;
        }

        public string GenerateFieldName(FieldInfo field)
        {
            return field.Name;
        }

        public string GeneratePropertyName(PropertyInfo property)
        {
            return property.Name;
        }

        public string GenerateMethodName(MethodBase method)
        {
            if(method.IsSpecialName && method.Name.StartsWith("op_"))
            { return GenerateOperatorName(method.Name.Substring(3)); }

            return method.Name;
        }

        public string GenerateOperatorName(string opMethodName)
        {
            if(Enum.TryParse(opMethodName, out Operator op))
            {
                switch(op)
                {
                    case Operator.UnaryPlus:
                        return "+";
                    case Operator.UnaryNegation:
                        return "-";
                    case Operator.LogicalNot:
                        return "!";
                    case Operator.OnesComplement:
                        return "~";
                    case Operator.Increment:
                        return "++";
                    case Operator.Decrement:
                        return "--";
                    case Operator.True:
                        return "true";
                    case Operator.False:
                        return "false";
                    case Operator.Addition:
                        return "+";
                    case Operator.Subtraction:
                        return "-";
                    case Operator.Multiply:
                        return "*";
                    case Operator.Division:
                        return "/";
                    case Operator.Modulus:
                        return "%";
                    case Operator.BitwiseAnd:
                        return "&";
                    case Operator.BitwiseOr:
                        return "|";
                    case Operator.ExclusiveOr:
                        return "^";
                    case Operator.LeftShift:
                        return "<<";
                    case Operator.RightShift:
                        return ">>";
                    case Operator.Equality:
                        return "==";
                    case Operator.Inequality:
                        return "!=";
                    case Operator.LessThan:
                        return "<";
                    case Operator.GreaterThan:
                        return ">";
                    case Operator.LessThanOrEqual:
                        return "<=";
                    case Operator.GreaterThanOrEqual:
                        return ">=";
                    case Operator.Explicit:
                        return "Explicit";
                    case Operator.Implicit:
                        return "Implicit";
                }
            }

            throw new NotSupportedException($"unknown operator '{opMethodName}'");
        }
    }
}
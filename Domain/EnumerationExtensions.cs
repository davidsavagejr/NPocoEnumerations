using System;
using System.Reflection;
using Headspring;

namespace Domain
{
    public static class EnumerationExtensions
    {
         public static bool IsEnumeration(this Type memberInfo)
         {
             return memberInfo.BaseType != null
                    && memberInfo.BaseType.IsGenericType
                    && memberInfo.BaseType.GetGenericTypeDefinition() == typeof (Enumeration<,>);
         }
    }
}
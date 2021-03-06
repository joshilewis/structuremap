using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StructureMap.Graph;

namespace StructureMap.TypeRules
{

    public static partial class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetSettableProperties(this Type type)
        {
            return type.GetTypeInfo()
                .DeclaredProperties
                .Where(mi => mi.CanWrite && mi.SetMethod.IsPublic && !mi.SetMethod.IsStatic && mi.SetMethod.GetParameters().Length == 1);
        } 

        private static bool ParametersMatch(this MethodBase method, ICollection<Type> parameterTypes)
        {
            var origin = (ICollection<ParameterInfo>)method.GetParameters();
            return origin.Count == parameterTypes.Count &&
                   origin.Select(pi => pi.ParameterType).SequenceEqual(parameterTypes);
        }
    }

    public static class AssemblyLoader
    {
        public static Assembly ByName(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }
    }
}

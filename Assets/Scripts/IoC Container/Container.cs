using System;
using System.Collections.Generic;

namespace IoC_Container
{
    public static class Container
    {
        private static readonly Dictionary<Type, object> Dependencies = new Dictionary<Type, object>();

        public static void Bind<T>(object tObject)
        {
            Dependencies.Add(typeof(T), tObject);
        }

        public static T Get<T>()
        {
            return (T) Dependencies[typeof(T)];
        }
    }
}
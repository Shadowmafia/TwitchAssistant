using System;
using System.Reflection;

namespace Tools
{

    public class SingletonBaseTemplate<T> where T : class
    {

        /// <summary>The one and only instance of the Singleton class.</summary>
        private static readonly Lazy<T> _instance = new Lazy<T>(
            () => (T)typeof(T).
                GetConstructor(
                BindingFlags.Instance
                        | BindingFlags.NonPublic
                , null
                , new Type[0]
              , null)
                ?.Invoke(null));

        /// <summary>Gets the singleton instance.</summary>
        public static T Instance => SingletonBaseTemplate<T>._instance.Value;
    }
}

using System;
using System.Linq;

namespace AggregateFramework
{
    internal class StateTypeExtractor<T> where T : IAggregate
    {
        /// <summary>
        /// For a concrete aggregate type T determine the type of its state
        /// </summary>
        /// <returns>The runtime type of the T's state</returns>
        public Type ExtractStateType()
        {
            var iAggregateGenericInterfacesCount = typeof(T).GetInterfaces().Count(IsGenericIAggregateInterface);
            if (iAggregateGenericInterfacesCount > 1) // T implements multiple IAggregate<> interfaces
            {
                throw new TypeArgumentException(string.Format("{0} must implement only one IAggregate<> generic interface.",
                    typeof(T).FullName));
            }

            var iAggregateInterface = typeof(T).GetInterfaces().SingleOrDefault(IsGenericIAggregateInterface);
            if (iAggregateInterface == null) // T does not implementing an IAggregate<> interface
            {
                throw new TypeArgumentException(string.Format("{0} must implement the IAggregate<> generic interface.",
                    typeof(T).FullName));
            }

            if (iAggregateInterface.GenericTypeArguments.Count() != 1) // T implements IAggregate<> but with the wrong number of type arguments
            {
                throw new TypeArgumentException(
                    string.Format(
                        "The IAggregate<> generic interface implemented by {0} may only have one generic type argument.",
                        typeof(T).FullName));
            }

            var stateType = iAggregateInterface.GenericTypeArguments[0];
            return stateType;
        }

        /// <summary>
        /// Determines if a given type is a generic IAggregate interface
        /// </summary>
        /// <param name="t">The type to check</param>
        /// <returns>True if t is a generic IAggregate interface, false it if is not</returns>
        private static bool IsGenericIAggregateInterface(Type t)
        {
            return t.IsGenericType && typeof(IAggregate<>).IsAssignableFrom(t.GetGenericTypeDefinition());
        }
    }
}

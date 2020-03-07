using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Runner
{
    public class ReflectionMapper : IMyMapper
    {
        private IDictionary<Type, IDictionary<Type, IDictionary<PropertyInfo, PropertyInfo>>> _propsCache 
            = new Dictionary<Type, IDictionary<Type, IDictionary<PropertyInfo, PropertyInfo>>>();

        public TTo Map<TFrom, TTo>(TFrom source)
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);

            var destInstance = Activator.CreateInstance<TTo>();

            var propsDict = _propsCache[fromType][toType];

            foreach(var propMapping in propsDict)
            {
                var value = propMapping.Key.GetValue(source);
                propMapping.Value.SetValue(destInstance, value);
            }

            return destInstance;
        }

        public void Register<TFrom, TTo>()
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);

            var fromProps = fromType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var toProps = toType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var fromDict = fromProps.ToDictionary(x => x.Name);
            var toDict = toProps.ToDictionary(x => x.Name);

            var intersects = fromDict.Keys.Intersect(toDict.Keys);

            if (!_propsCache.ContainsKey(fromType))
                _propsCache[fromType] = new Dictionary<Type, IDictionary<PropertyInfo, PropertyInfo>>();

            if (!_propsCache[fromType].ContainsKey(toType))
                _propsCache[fromType][toType] = new Dictionary<PropertyInfo, PropertyInfo>();

            _propsCache[fromType][toType] = intersects.ToDictionary(x => fromDict[x], x => toDict[x]);
        }
    }
}

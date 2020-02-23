using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Runner
{
    public class ExpressionMapper : IMyMapper
    {
        private IDictionary<Type, IDictionary<Type, Delegate>> _propsCache
    = new Dictionary<Type, IDictionary<Type, Delegate>>();

        public TTo Map<TFrom, TTo>(TFrom source)
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);

            var func = (Func<TFrom, TTo>)_propsCache[fromType][toType];
            return func(source);
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

            var instanceExpression = Expression.New(toType);

            var sourceParam = Expression.Parameter(fromType);

            var initExpression = Expression.MemberInit(instanceExpression, 
                intersects.Select(x => 
                    Expression.Bind(toDict[x], Expression.Property(sourceParam, fromDict[x]))));

            var lambda = Expression.Lambda(initExpression, sourceParam);

            if (!_propsCache.ContainsKey(fromType))
                _propsCache[fromType] = new Dictionary<Type, Delegate>();

            _propsCache[fromType][toType] = lambda.Compile();
        }
    }
}

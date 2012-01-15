using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy;

namespace OakenShield
{
    public class FilteringInterceptorSelector : IInterceptorSelector
    {
        private readonly IDictionary<string, LambdaExpression> _predicates;

        public FilteringInterceptorSelector(IDictionary<string,LambdaExpression> predicates)
        {
            _predicates = predicates;
        }

        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var filteringInterceptor = interceptors.OfType<FilteringInterceptor>().SingleOrDefault();
            if(filteringInterceptor != null)
            {
                filteringInterceptor.PredicatesToSatisfy = _predicates;
            }
            return interceptors;
        }
    }
}
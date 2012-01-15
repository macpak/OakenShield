using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OakenShield
{
    public class MethodDescriptor
    {
        private readonly MethodInfo _method;
        private readonly ParameterInfo[] _parameters;
        private readonly IDictionary<string, LambdaExpression> _predicates;
        private readonly Type _declaringType;

        public MethodDescriptor(MethodInfo method)
        {
            _method = method;
            _parameters = _method.GetParameters();
            _declaringType = _method.DeclaringType;

            _predicates = new Dictionary<string, LambdaExpression>();
        }

        public Type DeclaringType
        {
            get { return _declaringType; }
        }

        public IDictionary<string, LambdaExpression> Predicates
        {
            get { return _predicates; }
        }

        public MethodDescriptor Arg<TArgType>(string argName, Expression<Func<TArgType, bool>> predicate)
        {
            var singleOrDefault = _parameters.SingleOrDefault(p => p.Name == argName);
            if(singleOrDefault == null)
                throw new ArgumentException(String.Format("Cannot find {0}", argName));

            Predicates[argName] = predicate;

            return this;


        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Castle.DynamicProxy;

namespace OakenShield
{
    public class FilteringInterceptor : IInterceptor
    {
        public IDictionary<string,LambdaExpression> PredicatesToSatisfy { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var parameterInfos = invocation.MethodInvocationTarget.GetParameters();
            for (var index   = 0; index < invocation.Arguments.Length; index++)
            {
                var argument = invocation.Arguments[index];
                var parameterInfo = parameterInfos[index];
                LambdaExpression expression;
                if(!PredicatesToSatisfy.TryGetValue(parameterInfo.Name, out expression))
                {
                    continue; 
                }
       
                var result = expression.Compile();

                var isPredicateSatisfied = (bool)result.DynamicInvoke(argument);
                if (isPredicateSatisfied == false)
                    throw new PredicatesNotSatisfiedException(string.Format("Could not proceeed because of argument {0}", parameterInfo.Name));
            }

            invocation.Proceed();
        }
    }
}
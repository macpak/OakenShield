using System;

namespace OakenShield
{
    public class TypeDescriptor<T>
    {
        public TypeDescriptor()
        {
            
        }

        public  MethodDescriptor Method(System.Linq.Expressions.Expression<Action<T>> expression)
        {
            var methodCallExpression = expression.Body as System.Linq.Expressions.MethodCallExpression;
            return (methodCallExpression == null)
                       ? null
                       : new MethodDescriptor(methodCallExpression.Method);
        }
    }

    //public class ArgumentDescriptor
    //{
    //    public ArgumentDescriptor(PropertyInfo)
    //    {
            
    //    }
    //}
}
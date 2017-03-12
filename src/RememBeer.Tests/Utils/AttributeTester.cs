using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RememBeer.Tests.Utils
{
    public static class AttributeTester
    {
        // Via http://stackoverflow.com/questions/8817031/how-to-check-if-method-has-an-attribute
        public static bool MethodHasAttribute(Expression<Action> expression, Type attributeType)
        {
            var method = MethodOf(expression);

            const bool includeInherited = false;
            return method.GetCustomAttributes(attributeType, includeInherited).Any();
        }

        private static MethodInfo MethodOf(Expression<Action> expression)
        {
            MethodCallExpression body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}

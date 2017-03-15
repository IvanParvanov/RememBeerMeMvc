using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

using NUnit.Framework;

using RememBeer.Common.Constants;

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

        public static bool ClassHasAttribute(Type classType, Type attributeType)
        {
            var attr = Attribute.GetCustomAttribute(classType, attributeType);
            return attr != null;
        }

        public static void EnsureClassHasAdminAuthorizationAttribute(Type classType)
        {
            var attr = Attribute.GetCustomAttribute(classType, typeof(AuthorizeAttribute)) as AuthorizeAttribute;

            Assert.IsNotNull(attr);
            Assert.AreEqual(attr.Roles, Constants.AdminRole);
        }
    }
}

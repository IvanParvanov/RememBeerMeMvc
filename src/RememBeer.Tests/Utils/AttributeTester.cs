using System;
using System.Linq;

namespace RememBeer.Tests.Utils
{
    public static class AttributeTester
    {
        public static bool MethodHasAttribute(Type baseType, string methodName, Type attributeType)
        {
            var method = baseType.GetMethods()
                                 .SingleOrDefault(x => x.Name == methodName);
            var attribute = method?.GetCustomAttributes(attributeType, true)
                                  .SingleOrDefault();

            return attribute != null;
        }
    }
}

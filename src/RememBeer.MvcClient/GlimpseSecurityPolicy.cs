using System.Diagnostics.CodeAnalysis;

using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;

using RememBeer.Common.Constants;

namespace RememBeer.MvcClient
{
    [ExcludeFromCodeCoverage]
    public class GlimpseSecurityPolicy : IRuntimePolicy
    {
        public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
        {
			 var httpContext = policyContext.GetHttpContext();
             if (!httpContext.User.IsInRole(Constants.AdminRole))
			 {
                 return RuntimePolicy.Off;
			 }

            return RuntimePolicy.On;
        }

        public RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource; }
        }
    }
}

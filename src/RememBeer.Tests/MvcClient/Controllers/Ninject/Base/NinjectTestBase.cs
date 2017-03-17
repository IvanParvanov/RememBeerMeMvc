using Ninject.MockingKernel.Moq;

using NUnit.Framework;

namespace RememBeer.Tests.MvcClient.Controllers.Ninject.Base
{
    public abstract class NinjectTestBase
    {
        public const string AjaxContextName = "ajax";
        public const string RegularContextName = "non-ajax";
        public const string UnauthContextName = "unauthorized";

        protected readonly MoqMockingKernel Kernel = new MoqMockingKernel();

        [TearDown]
        public void ResetMocks()
        {
            this.Kernel.Reset();
        }

        [OneTimeSetUp]
        public abstract void Init();
    }
}

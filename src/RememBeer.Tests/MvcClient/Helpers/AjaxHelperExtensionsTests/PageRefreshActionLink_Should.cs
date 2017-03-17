using System.Web.Mvc;

using Moq;

using NUnit.Framework;

using RememBeer.MvcClient.Helpers;

namespace RememBeer.Tests.MvcClient.Helpers.AjaxHelperExtensionsTests
{
    [TestFixture]
    public class PageRefreshActionLink_Should
    {
        [TestCase("text", "class")]
        public void SetLinkTextAndClassCorrectly(string linkText, string className)
        {
            var helper = new AjaxHelper(new Mock<ViewContext>().Object, new Mock<IViewDataContainer>().Object);
            var result = helper.PageRefreshActionLink(linkText, className, It.IsAny<string>(), It.IsAny<string>(), null);

            var actual = result.ToString();

            StringAssert.Contains($" class=\"{className}\" ", actual);
            StringAssert.Contains($">{linkText}</a>", actual);
        }
    }
}

using System.Collections.Specialized;
using System.Web;

namespace RememBeer.Tests.Utils.MockedClasses
{
    public class MockedHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection queryString;

        public MockedHttpRequest()
        {
            this.queryString = new NameValueCollection();
        }

        public override NameValueCollection QueryString => this.queryString;
    }
}
